using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.ViewModels
{
    class EndPageVM : BaseVM
    {
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public char Winner { get; set; }

        public EndPageVM()
        {

        }

        public EndPageVM(int minutes, int seconds, char winner)
        {
            Minutes = minutes;
            Seconds = seconds;
            Winner = winner;
        }
    }
}
