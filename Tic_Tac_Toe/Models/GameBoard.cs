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
        /// character of empty filed
        /// </summary>
        private readonly char emptyFiled = ' ';

        /// <summary>
        /// size of the game board
        /// </summary>
        private readonly int size;

        public GameBoard(int size)
        {
            this.size = size;
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                var row = new ObservableCollection<char>();

                for (int j = 0; j < size; j++)
                {
                    row.Add(emptyFiled);
                }
                Board.Add(row);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord1"></param>
        /// <param name="coord2"></param>
        public void Place(int coord1, int coord2)
        {
            if (Board[coord1][coord2] == emptyFiled)
            {
                Board[coord1][coord2] = player;
            }

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
        public bool IsInBoard(int coord1, int coord2)
        {
            return (coord1 >= 0) && (coord1 < size) && (coord2 >= 0) && (coord2 < size);
        }

        /// <summary>
        /// is this position inside game board?
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsInBoard(Point point)
        {
            return IsInBoard(point.Coord1, point.Coord2);
        }

        /// <summary>
        /// determines whether there is a winner
        /// </summary>
        /// <param name="coord1">coord1 of last move</param>
        /// <param name="coord2">coord2 of last move</param>
        /// <returns></returns>
        public bool IsWinner(int coord1, int coord2)
        {
            var winningPositions = new List<List<Point>>();

            // fill winning positions
            for (int i = -4; i <= 0; i++)
            {
                // horizontal
                List<Point> list = new List<Point>
                {
                    new Point(coord1,coord2+i),
                    new Point(coord1,coord2+i+1),
                    new Point(coord1,coord2+i+2),
                    new Point(coord1,coord2+i+3),
                    new Point(coord1,coord2+i+4),
                };
                if (IsInBoard(list[0]) && IsInBoard(list[4]))
                {
                    winningPositions.Add(list);
                }

                // vertical
                list = new List<Point>
                {
                    new Point(coord1+i,coord2),
                    new Point(coord1+i+1,coord2),
                    new Point(coord1+i+2,coord2),
                    new Point(coord1+i+3,coord2),
                    new Point(coord1+i+4,coord2),
                };
                if (IsInBoard(list[0]) && IsInBoard(list[4]))
                {
                    winningPositions.Add(list);
                }

                // diagonal 1 (top left -> right bottom)
                list = new List<Point>
                {
                    new Point(coord1+i,coord2+i),
                    new Point(coord1+i+1,coord2+i+1),
                    new Point(coord1+i+2,coord2+i+2),
                    new Point(coord1+i+3,coord2+i+3),
                    new Point(coord1+i+4,coord2+i+4),
                };
                if (IsInBoard(list[0]) && IsInBoard(list[4]))
                {
                    winningPositions.Add(list);
                }

                // diagonal 2 (bottom left -> top right)
                list = new List<Point>
                {
                    new Point(coord2-i,coord2+i),
                    new Point(coord2-(i+1),coord2+i+1),
                    new Point(coord2-(i+2),coord2+i+2),
                    new Point(coord2-(i+3),coord2+i+3),
                    new Point(coord2-(i+4),coord2+i+4),
                };
                if (IsInBoard(list[0]) && IsInBoard(list[4]))
                {
                    winningPositions.Add(list);
                }
            }

            foreach (var list in winningPositions)
            {
                List<char> values = new List<char>();
                for (int i = 0; i < list.Count; i++)
                {
                    int c1 = list[i].Coord1;
                    int c2 = list[i].Coord2;
                    values.Add(Board[c1][c2]);
                }
                if (values.All(x => x == values[0]) && (values[0] == 'O' || values[0] == 'X'))
                    return true;
            }
            return false;
        }

    }

    public struct Point
    {
        public int Coord1 { get; }
        public int Coord2 { get; }

        public Point(int coord1, int coord2)
        {
            Coord1 = coord1;
            Coord2 = coord2;
        }
    }
}
