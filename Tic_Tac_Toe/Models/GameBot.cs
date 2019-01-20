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

        #region Priorities

        // priority of movement that makes 5,4,3,2 characters in a row
        private readonly int makeFivePriority = 1000000; // victory
        private readonly int makeFourPriority = 5000;
        private readonly int makeThreePriority = 500;
        private readonly int makeTwoPriority = 50;

        // priority of movement that makes 4,3,2,1 characters in a row
        private readonly int blockFourPriority = 10000;
        private readonly int blockThreePriority = 1000;
        private readonly int blockTwoPriority = 100;
        private readonly int blockOnePriority = 10;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">gameboard</param>
        /// <param name="botCharacter">has bot X/O character?</param>
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
        /// check if <paramref name="positionSequence"/> is blocked on the other end than <paramref name="position"/>
        /// </summary>
        /// <param name="position">position of the possible movement</param>
        /// <param name="positionSequence">characters in a row, (placing at <paramref name="position"/> makes <paramref name="positionSequence"/> longer)</param>
        /// <param name="sequenceCharacter">character of sequence (X/O), ordered from nearest to furthest to <paramref name="position"/></param>
        /// <param name="blockingCharacter">character of opposite player (opposite than <paramref name="sequenceCharacter"/>)</param>
        /// <returns></returns>
        private bool IsBlocked(Point position, List<Point> positionSequence, char sequenceCharacter, char blockingCharacter)
        {
            int differenceCoord1, differenceCoord2;
            Point blockingCell = new Point();
            Point lastInSequence = positionSequence.Last();

            differenceCoord1 = lastInSequence.Coord1 - position.Coord1;
            differenceCoord2 = lastInSequence.Coord2 - position.Coord2;

            // get blockingCell position
            if (differenceCoord1 < 0)
                blockingCell.Coord1 = lastInSequence.Coord1 - 1;
            else if (differenceCoord1 > 0)
                blockingCell.Coord1 = lastInSequence.Coord1 + 1;
            else
                blockingCell.Coord1 = lastInSequence.Coord1;

            if (differenceCoord2 < 0)
                blockingCell.Coord2 = lastInSequence.Coord2 - 1;
            else if (differenceCoord2 > 0)
                blockingCell.Coord2 = lastInSequence.Coord2 + 1;
            else
                blockingCell.Coord2 = lastInSequence.Coord2;

            // blocked by edge of the board
            if (!gameBoard.IsInBoard(blockingCell.Coord1, blockingCell.Coord2))
                return true;

            return gameBoard.Board[blockingCell.Coord1][blockingCell.Coord2].Content == blockingCharacter;
        }

        /// <summary>
        /// check if movement on <paramref name="position"/> makes/blocks specified number of characters (<paramref name="charactersInRow"/>),
        /// according to that set <paramref name="priority"/>/<paramref name="blockedPriority"/> of <paramref name="position"/>
        /// </summary>
        /// <param name="position">position of the movement</param>
        /// <param name="charactersInRow">number of characters in row to seek</param>
        /// <param name="priority">priority to set if conditions are satisfied</param>
        /// <param name="blockedPriority">priority to set if conditions are satisfied, but sequence of characters are blocked from the other side</param>
        /// <param name="character">character to compare with (X/O), X - check for AI's characters in a row, O - block users characters in a row </param>
        private void SetPriority(Point position, int charactersInRow, int priority, int blockedPriority, char character)
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
                    char oppositeCharacter = (character == 'X') ? 'O' : 'X';
                    if (IsBlocked(new Point(i, j), points, character, oppositeCharacter))
                    {
                        priorityMap[i, j] += blockedPriority;
                    }
                    else
                    {
                        priorityMap[i, j] += priority;
                    }
                }
            }
        }

        /// <summary>
        /// count priority of position
        /// </summary>
        /// <param name="position"></param>if
        private void CountPositionPriority(Point position)
        {
            // Block opponent's movements
            SetPriority(position, 4, blockFourPriority, blockFourPriority, playerCharacter);
            SetPriority(position, 3, blockThreePriority, blockTwoPriority, playerCharacter);
            SetPriority(position, 2, blockTwoPriority, blockOnePriority, playerCharacter);
            SetPriority(position, 1, blockOnePriority, 0, playerCharacter); // Block opponent's 1 character in row ->
                                                                            // AI makes movements in the same area as user

            // Make a move
            SetPriority(position, 4, makeFivePriority, makeFivePriority, botCharacter);
            SetPriority(position, 3, makeFourPriority, makeThreePriority, botCharacter);
            SetPriority(position, 2, makeThreePriority, makeTwoPriority, botCharacter);
            SetPriority(position, 1, makeTwoPriority, 0, botCharacter);
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