using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Tic_Tac_Toe.Models
{
    internal class GameBoard : ObservableObject
    {
        /// <summary>
        /// game board
        /// </summary>
        public ObservableCollection<ObservableCollection<Cell>> Board { get; } = new ObservableCollection<ObservableCollection<Cell>>();

        /// <summary>
        /// character of current player
        /// </summary>
        private char player = 'O';

        /// <summary>
        /// character of empty filed
        /// </summary>
        private readonly char emptyField = ' ';

        /// <summary>
        /// color of winning cell
        /// </summary>
        private readonly Brush winningCellColor;

        /// <summary>
        /// Size of the game board
        /// </summary>
        public int Size { get; }

        private char? winner = null;

        /// <summary>
        /// Is winner X/O?
        /// </summary>
        public char? Winner
        {
            get
            {
                return winner;
            }
            private set
            {
                winner = value;
                RaisePropertyChanged();
            }
        }

        public GameBoard(int size)
        {
            this.Size = size;
            winningCellColor = new SolidColorBrush(Color.FromRgb(50, 205, 50));

            for (int i = 0; i < size; i++)
            {
                var row = new ObservableCollection<Cell>();

                for (int j = 0; j < size; j++)
                {
                    row.Add(new Cell(emptyField, i, j));
                }
                Board.Add(row);
            }
        }

        /// <summary>
        /// is this position empty?
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool IsEmpty(int coord1, int coord2)
        {
            return Board[coord1][coord2].Content == emptyField;
        }

        /// <summary>
        /// is the cell with cellID empty?
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool IsEmpty(int cellID)
        {
            Cell cell = Find(cellID);
            return IsEmpty(cell.Coord1, cell.Coord2);
        }

        /// <summary>
        /// place X/O on field with coordinates
        /// </summary>
        /// <param name="coord1">coordinates of field</param>
        /// <param name="coord2">coordinates of field</param>
        public void Place(int coord1, int coord2)
        {
            if (IsEmpty(coord1,coord2) && Winner == null)
            {
                Cell selectedCell = Board[coord1][coord2];
                Board[coord1][coord2] = new Cell(player, selectedCell.Coord1, selectedCell.Coord2);

                SwitchPlayers();
                IsWinner(coord1, coord2);
            }
        }

        /// <summary>
        /// place X/O on cell with cellID
        /// </summary>
        /// <param name="cellID"></param>
        public void Place(int cellID)
        {
            Cell cell = Find(cellID);
            Place(cell.Coord1, cell.Coord2);
        }

        /// <summary>
        /// find array with specified ID in board
        /// </summary>
        /// <param name="id"></param>
        private Cell Find(int id)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Board[i][j].Id == id)
                        return Board[i][j];
                }
            }
            return null;
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
            return (coord1 >= 0) && (coord1 < Size) && (coord2 >= 0) && (coord2 < Size);
        }

        /// <summary>
        /// is this position inside game board?
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsInBoard(Cell cell)
        {
            return IsInBoard(cell.Coord1, cell.Coord2);
        }

        /// <summary>
        /// get winning positions list
        /// </summary>
        /// <param name="coord1">coord1 of last move</param>
        /// <param name="coord2">coord2 of last move</param>
        /// <returns></returns>
        private List<List<Point>> GetWinningPositions(int coord1, int coord2)
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
                if (IsInBoard(list[0].Coord1, list[0].Coord2) && IsInBoard(list[4].Coord1, list[4].Coord2))
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
                if (IsInBoard(list[0].Coord1, list[0].Coord2) && IsInBoard(list[4].Coord1, list[4].Coord2))
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
                if (IsInBoard(list[0].Coord1, list[0].Coord2) && IsInBoard(list[4].Coord1, list[4].Coord2))
                {
                    winningPositions.Add(list);
                }

                // diagonal 2 (bottom left -> top right)
                list = new List<Point>
                {
                    new Point(coord1-i,coord2+i),
                    new Point(coord1-(i+1),coord2+i+1),
                    new Point(coord1-(i+2),coord2+i+2),
                    new Point(coord1-(i+3),coord2+i+3),
                    new Point(coord1-(i+4),coord2+i+4),
                };
                if (IsInBoard(list[0].Coord1, list[0].Coord2) && IsInBoard(list[4].Coord1, list[4].Coord2))
                {
                    winningPositions.Add(list);
                }
            }

            return winningPositions;
        }

        /// <summary>
        /// determines whether there is a winner
        /// </summary>
        /// <param name="coord1">coord1 of last move</param>
        /// <param name="coord2">coord2 of last move</param>
        /// <returns></returns>
        public void IsWinner(int coord1, int coord2)
        {
            var winningPositions = GetWinningPositions(coord1, coord2);

            foreach (var list in winningPositions)
            {
                List<Cell> cells = new List<Cell>();

                for (int i = 0; i < list.Count; i++)
                {
                    int c1 = list[i].Coord1;
                    int c2 = list[i].Coord2;
                    cells.Add(Board[c1][c2]);
                }
                if (cells.All(x => x.Content == cells[0].Content) && (cells[0].Content == 'O' || cells[0].Content == 'X'))
                {
                    Winner = cells[0].Content;

                    foreach (Cell winningCell in cells)
                    {
                        Board[winningCell.Coord1][winningCell.Coord2] = new Cell(winningCell.Content,
                            winningCell.Coord1, winningCell.Coord2, winningCellColor);
                    }
                }
            }
        }

    }

    public struct Point
    {
        public int Coord1 { get; set; }
        public int Coord2 { get; set; }

        public Point(int coord1, int coord2) : this()
        {
            Coord1 = coord1;
            Coord2 = coord2;
        }
    }
}