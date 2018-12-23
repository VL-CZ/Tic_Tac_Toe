using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Models
{
    class GameBoard
    {
        /// <summary>
        /// game board
        /// </summary>
        public ObservableCollection<ObservableCollection<char>> Board { get; } = new ObservableCollection<ObservableCollection<char>>();
        
        /// <summary>
        /// character of current player
        /// </summary>
        private char player = 'O';

        /// <summary>
        /// size of the game board
        /// </summary>
        private readonly int size;

        public GameBoard(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Board.Add(new ObservableCollection<char>());

                for (int j = 0; j < size; j++)
                {
                    Board[i].Add('O');
                }
            }
            this.size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        public void Place(int coord1, int coord2)
        {
            Board[coord1][coord2] = player;

            SwitchPlayers();
        }

        /// <summary>
        /// switch X and O players
        /// </summary>
        private void SwitchPlayers()
        {
            if (player == 'O')
                player = 'X';
            else if (player == 'X')
                player = 'O';
        }

        /// <summary>
        /// is this position inside game board?
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        /// <returns></returns>
        public bool IsInBoard(int coord1,int coord2)
        {
            return ((coord1 >= 0) && (coord1 < size) && (coord2 >= 0) && (coord2 < size));
        }

        /// <summary>
        /// determines whether there is a winner
        /// </summary>
        /// <returns></returns>
        public bool IsWinner()
        {
            return false;
        }

    }
}
