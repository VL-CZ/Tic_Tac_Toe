namespace Tic_Tac_Toe.Models
{
    public class Cell
    {
        /// <summary>
        /// dimension of the game board
        /// </summary>
        private readonly int boardDimension = 25;

        /// <summary>
        /// id of the cell (initialized in ctor)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// content of the cell
        /// </summary>
        public char Content { get; set; }

        /// <summary>
        /// vertical (0-highest)
        /// </summary>
        public int Coord1 { get; set; }

        /// <summary>
        /// horizontal (0-leftmost)
        /// </summary>
        public int Coord2 { get; set; }

        public Cell(char content, int coord1, int coord2)
        {
            Id = coord1 * boardDimension + coord2;
            Content = content;
            Coord1 = coord1;
            Coord2 = coord2;
        }

        public override string ToString()
        {
            return Content.ToString();
        }
    }
}