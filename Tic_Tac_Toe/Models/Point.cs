using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Models
{
    public class Point
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
