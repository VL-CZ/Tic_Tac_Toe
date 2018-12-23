using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Models
{
    public class Cell
    {
        public string Id { get; set; }
        public char Content { get; set; }
        public int Coord1 { get; set; }
        public int Coord2 { get; set; }
        public Cell(char content,int coord1, int coord2)
        {
            Id = (Guid.NewGuid()).ToString();
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
