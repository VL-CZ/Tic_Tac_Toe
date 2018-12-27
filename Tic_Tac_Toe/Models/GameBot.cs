using System.Collections.Generic;
using System.Linq;

namespace Tic_Tac_Toe.Models
{
    internal class GameBot
    {
        /// <summary>
        /// priorites of boxes
        /// </summary>
        private int[,] priorityMap;

        /// <summary>
        /// game board
        /// </summary>
        private readonly GameBoard gameBoard;

        /// <summary>
        /// character (X/O) of bot
        /// </summary>
        private readonly char botCharacter;

        /// <summary>
        /// character (X/O) of player
        /// </summary>
        private readonly char playerCharacter;

        private readonly int makeFivePriority = 1000000;
        private readonly int makeFourPriority = 5000;
        private readonly int makeThreePriority = 500;
        private readonly int makeTwoPriority = 50;

        private readonly int blockFourPriority = 10000;
        private readonly int blockThreePriority = 1000;
        private readonly int blockTwoPriority = 100;
        private readonly int blockOnePriority = 10;

        public GameBot(GameBoard game, char botCharacter)
        {
            priorityMap = new int[game.Size, game.Size];
            gameBoard = game;
            this.botCharacter = botCharacter;
            if (botCharacter == 'X')
                playerCharacter = 'O';
            else if (botCharacter == 'O')
                playerCharacter = 'X';
        }

        /// <summary>
        /// block/ attack
        /// </summary>
        /// <param name="position">position of the movement</param>
        /// <param name="charactersInRow">number of characters in row to look for</param>
        /// <param name="priority">priority to set if conditions are satisfied</param>
        /// <param name="character">character to compare with</param>
        private void Block(Point position, int charactersInRow, int priority, char character)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            var rightPositions = new List<Point>();
            var leftPositions = new List<Point>();
            var topPositions = new List<Point>();
            var bottomPositions = new List<Point>();
            var bottomRightPositions = new List<Point>();
            var bottomLeftPositions = new List<Point>();
            var topLeftPositions = new List<Point>();
            var topRightPositions = new List<Point>();

            for (int x = 1; x <= charactersInRow; x++)
            {
                rightPositions.Add(new Point(i, j + x));
                leftPositions.Add(new Point(i, j - x));
                topPositions.Add(new Point(i - x, j));
                bottomPositions.Add(new Point(i + x, j));
                bottomRightPositions.Add(new Point(i + x, j + x));
                bottomLeftPositions.Add(new Point(i + x, j - x));
                topLeftPositions.Add(new Point(i - x, j - x));
                topRightPositions.Add(new Point(i - x, j + x));
            }

            positions.Add(leftPositions);
            positions.Add(rightPositions);
            positions.Add(topPositions);
            positions.Add(bottomPositions);
            positions.Add(topLeftPositions);
            positions.Add(topRightPositions);
            positions.Add(bottomLeftPositions);
            positions.Add(bottomRightPositions);

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == character))
                {
                    priorityMap[i, j] += priority;
                }
            }
        }

        /// <summary>
        /// count priority of position
        /// </summary>
        /// <param name="position"></param>if
        private void CountPositionPriority(Point position)
        {
            Block(position, 4, blockFourPriority, playerCharacter);
            Block(position, 3, blockThreePriority, playerCharacter);
            Block(position, 2, blockTwoPriority, playerCharacter);
            Block(position, 1, blockOnePriority, playerCharacter);

            Block(position, 5, makeFivePriority, botCharacter);
            Block(position, 4, makeFourPriority, botCharacter);
            Block(position, 3, makeThreePriority, botCharacter);
            Block(position, 2, makeTwoPriority, botCharacter);
        }

        /// <summary>
        /// count priority of boxes in gameboard
        /// </summary>
        private void CountPriority()
        {
            priorityMap = new int[gameBoard.Size, gameBoard.Size];
            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    if (gameBoard.IsEmpty(i, j))
                        CountPositionPriority(new Point(i, j));
                }
            }
        }

        /// <summary>
        /// execute move with highest priority
        /// </summary>
        public void BestMove()
        {
            CountPriority();

            int max = -1000;
            Cell bestCell = gameBoard.Board[0][0];

            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    Cell cell = gameBoard.Board[i][j];
                    if (gameBoard.IsEmpty(cell.Coord1, cell.Coord2) && priorityMap[i, j] > max)
                    {
                        max = priorityMap[i, j];
                        bestCell = cell;
                    }
                }
            }

            gameBoard.Place(bestCell.Coord1, bestCell.Coord2);
        }
    }
}