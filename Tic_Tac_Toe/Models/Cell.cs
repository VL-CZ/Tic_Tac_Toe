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
        public Cell(char content)
        {
            Id = (Guid.NewGuid()).ToString();
            Content = content;
        }

        public override string ToString()
        {
            return Content.ToString();
        }
    }
}
