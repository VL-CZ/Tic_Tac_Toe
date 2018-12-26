using System;
using System.Collections.Generic;

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

        private readonly int oneMissingPriority = 1000000;

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
        /// check if points satisfy all conditions
        /// </summary>
        /// <param name="points">points to check</param>
        /// <param name="position">position of possible move</param>
        private void CheckPoints(List<Point> points, Point position)
        {
            if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                gameBoard.Board[x.Coord1][x.Coord2].Content == botCharacter))
            {
                priorityMap[position.Coord1, position.Coord2] += oneMissingPriority;
            }
        }

        /// <summary>
        /// count priority of position
        /// </summary>
        /// <param name="position"></param>
        private void CountPositionPriority(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            // vertical
            List<Point> points = new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
                new Point(i+3,j),
                new Point(i+4,j)
            };
            CheckPoints(points, new Point(i, j));

            points = new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
                new Point(i-3,j),
                new Point(i-4,j)
            };
            CheckPoints(points, new Point(i, j));

            // horizontal
            points = new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
                new Point(i,j+3),
                new Point(i,j+4)
            };
            CheckPoints(points, new Point(i, j));

            points = new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
                new Point(i,j-3),
                new Point(i,j-4)
            };
            CheckPoints(points, new Point(i, j));

            // diagonal top left -> bottom right 
            points = new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
                new Point(i+3,j+3),
                new Point(i+4,j+4)
            };
            CheckPoints(points, new Point(i, j));

            points = new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
                new Point(i-3,j+3),
                new Point(i-4,j+4)
            };
            CheckPoints(points, new Point(i, j));

            // diagonal top right -> bottom left
            points = new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
                new Point(i-3,j+3),
                new Point(i-4,j+4)
            };
            CheckPoints(points, new Point(i, j));

            points = new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
                new Point(i+3,j-3),
                new Point(i+4,i-4)
            };
            CheckPoints(points, new Point(i, j));
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
                    if (gameBoard.IsInBoard(i, j + 1) && gameBoard.Board[i][j + 1].Content == playerCharacter)
                        priorityMap[i, j] += 10000;

                    CountPositionPriority(new Point(i, j));
                }
            }
            Random random = new Random();
            int coord1 = random.Next(25);
            int coord2 = random.Next(25);
            priorityMap[coord1, coord2] = 1000;
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