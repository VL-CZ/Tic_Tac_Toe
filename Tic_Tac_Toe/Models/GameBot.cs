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

        private void Block(Point position, int characters, int priority, char character)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            List<Point> rightPositions = new List<Point>();
            List<Point> leftPositions = new List<Point>();
            List<Point> topPositions = new List<Point>();
            List<Point> bottomPositions = new List<Point>();
            List<Point> bottomRightPositions = new List<Point>();
            List<Point> bottomLeftPositions = new List<Point>();
            List<Point> topLeftPositions = new List<Point>();
            List<Point> topRightPositions = new List<Point>();

            for (int x = 0; x < characters; x++)
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

        private void BlockFour(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
                new Point(i,j+3),
                new Point(i,j+4)
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
                new Point(i,j-3),
                new Point(i,j-4)
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
                new Point(i-3,j),
                new Point(i-4,j)
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
                new Point(i+3,j),
                new Point(i+4,j)
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
                new Point(i+3,j+3),
                new Point(i+4,j+4)
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
                new Point(i-2,j-2),
                new Point(i-3,j-3),
                new Point(i-4,j-4)
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
                new Point(i+3,j-3),
                new Point(i+4,j-4)
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
                new Point(i-3,j+3),
                new Point(i-4,j+4)
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == playerCharacter))
                {
                    priorityMap[i, j] += blockFourPriority;
                }
            }
        }

        private void BlockThree(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
                new Point(i,j+3)
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
                new Point(i,j-3)
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
                new Point(i-3,j)
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
                new Point(i+3,j)
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
                new Point(i+3,j+3),
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
                new Point(i-2,j-2),
                new Point(i-3,j-3),
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
                new Point(i+3,j-3),
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
                new Point(i-3,j+3),
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == playerCharacter))
                {
                    priorityMap[i, j] += blockThreePriority;
                }
            }
        }

        private void BlockTwo(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
                new Point(i-2,j-2),
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == playerCharacter))
                {
                    priorityMap[i, j] += blockTwoPriority;
                }
            }
        }

        private void BlockOne(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == playerCharacter))
                {
                    priorityMap[i, j] += blockOnePriority;
                }
            }
        }

        private void MakeFive(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
                new Point(i,j+3),
                new Point(i,j+4),
                new Point(i,j+5)
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
                new Point(i,j-3),
                new Point(i,j-4),
                new Point(i,j-5)
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
                new Point(i-3,j),
                new Point(i-4,j),
                new Point(i-5,j)
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
                new Point(i+3,j),
                new Point(i+4,j),
                new Point(i+5,j)
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
                new Point(i+3,j+3),
                new Point(i+4,j+4),
                new Point(i+5,j+5),
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
                new Point(i-2,j-2),
                new Point(i-3,j-3),
                new Point(i-4,j-4),
                new Point(i-5,j-5)
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
                new Point(i+3,j-3),
                new Point(i+4,j-4),
                new Point(i+5,j-5),
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
                new Point(i-3,j+3),
                new Point(i-4,j+4),
                new Point(i-5,j+5),
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == botCharacter))
                {
                    priorityMap[i, j] += makeFivePriority;
                }
            }
        }

        private void MakeFour(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
                new Point(i,j+3),
                new Point(i,j+4)
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
                new Point(i,j-3),
                new Point(i,j-4)
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
                new Point(i-3,j),
                new Point(i-4,j)
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
                new Point(i+3,j),
                new Point(i+4,j)
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
                new Point(i+3,j+3),
                new Point(i+4,j+4)
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
                new Point(i-2,j-2),
                new Point(i-3,j-3),
                new Point(i-4,j-4)
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
                new Point(i+3,j-3),
                new Point(i+4,j-4)
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
                new Point(i-3,j+3),
                new Point(i-4,j+4)
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == botCharacter))
                {
                    priorityMap[i, j] += makeFourPriority;
                }
            }
        }

        private void MakeThree(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
                new Point(i,j+3)
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
                new Point(i,j-3)
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
                new Point(i-3,j)
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
                new Point(i+3,j)
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
                new Point(i+3,j+3),
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
                new Point(i-2,j-2),
                new Point(i-3,j-3),
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
                new Point(i+3,j-3),
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
                new Point(i-3,j+3),
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == botCharacter))
                {
                    int x = points.Last().Coord1 - points.First().Coord1;
                    int y = points.Last().Coord2 - points.First().Coord2;
                    int a, b;
                    if (x > 0)
                    {
                        a = points.Last().Coord1 + 1;
                    }
                    else
                    {
                        a = points.Last().Coord1 - 1;
                    }

                    if (y > 0)
                    {
                        b = points.Last().Coord2 + 1;
                    }
                    else
                    {
                        b = points.Last().Coord2 - 1;
                    }

                    Point behindPosition = new Point(a, b);
                    priorityMap[i, j] += makeThreePriority;
                }
            }
        }

        private void MakeTwo(Point position)
        {
            int i = position.Coord1;
            int j = position.Coord2;

            var positions = new List<List<Point>>();

            // right
            positions.Add(new List<Point>
            {
                new Point(i,j+1),
                new Point(i,j+2),
            });

            // left
            positions.Add(new List<Point>
            {
                new Point(i,j-1),
                new Point(i,j-2),
            });

            // up
            positions.Add(new List<Point>
            {
                new Point(i-1,j),
                new Point(i-2,j),
            });

            // down
            positions.Add(new List<Point>
            {
                new Point(i+1,j),
                new Point(i+2,j),
            });

            // bottom right
            positions.Add(new List<Point>
            {
                new Point(i+1,j+1),
                new Point(i+2,j+2),
            });

            // top left
            positions.Add(new List<Point>
            {
                new Point(i-1,j-1),
                new Point(i-2,j-2),
            });

            // bottom left
            positions.Add(new List<Point>
            {
                new Point(i+1,j-1),
                new Point(i+2,j-2),
            });

            // top right
            positions.Add(new List<Point>
            {
                new Point(i-1,j+1),
                new Point(i-2,j+2),
            });

            foreach (var points in positions)
            {
                if (points.TrueForAll(x => gameBoard.IsInBoard(x.Coord1, x.Coord2) &&
                    gameBoard.Board[x.Coord1][x.Coord2].Content == botCharacter))
                {
                    priorityMap[i, j] += makeTwoPriority;
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