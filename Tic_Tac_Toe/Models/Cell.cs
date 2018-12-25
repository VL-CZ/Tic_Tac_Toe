using System.Windows.Media;

namespace Tic_Tac_Toe.Models
{
    public class Cell : ObservableObject
    {
        /// <summary>
        /// dimension of the game board
        /// </summary>
        private readonly int boardDimension = 25;

        private int id;

        /// <summary>
        /// id of the cell (initialized in ctor)
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged();
            }
        }

        private char content;

        /// <summary>
        /// content of the cell
        /// </summary>
        public char Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                RaisePropertyChanged();
            }
        }

        private int coord1;

        /// <summary>
        /// vertical (0-highest)
        /// </summary>
        public int Coord1
        {
            get
            {
                return coord1;
            }
            set
            {
                coord1 = value;
                RaisePropertyChanged();
            }
        }

        private int coord2;

        /// <summary>
        /// horizontal (0-leftmost)
        /// </summary>
        public int Coord2
        {
            get
            {
                return coord2;
            }
            set
            {
                coord2 = value;
                RaisePropertyChanged();
            }
        }

        private Brush background;

        /// <summary>
        /// background
        /// </summary>
        public Brush Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                RaisePropertyChanged();
            }
        }

        public Cell(char content, int coord1, int coord2) : this(content, coord1, coord2, new SolidColorBrush(Color.FromRgb(221, 221, 221)))
        {
        }

        public Cell(char content, int coord1, int coord2, Brush background)
        {
            Id = coord1 * boardDimension + coord2;
            Content = content;
            Coord1 = coord1;
            Coord2 = coord2;
            Background = background;
        }

        public override string ToString()
        {
            return Content.ToString();
        }
    }
}