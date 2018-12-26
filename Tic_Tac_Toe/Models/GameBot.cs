using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Models
{
    class GameBot
    {
        private readonly int[,] priorityMap;
        private readonly GameBoard gameBoard;

        public GameBot(GameBoard game)
        {
            priorityMap = new int[game.Size, game.Size];
            gameBoard = game;
        }

        /// <summary>
        /// count priority of boxes in gameboard
        /// </summary>
        private void CountPriority()
        {
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

            int max = 0;
            Cell bestCell = gameBoard.Board[0][0];

            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    Cell cell = gameBoard.Board[i][j];
                    if (cell.Content == gameBoard.EmptyField && priorityMap[i, j] > max)
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
