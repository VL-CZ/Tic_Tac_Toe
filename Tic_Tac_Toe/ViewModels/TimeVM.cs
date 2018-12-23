using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.ViewModels
{
    class TimeVM : BaseVM
    {
        public int minutes = 0;
        public int Minutes
        {
            get
            {
                minutes = minutes % 60;
                return minutes;
            }
            set
            {
                minutes = value;
                RaisePropertyChanged();
            }
        }

        public int seconds = 0;
        public int Seconds
        {
            get
            {
                seconds = seconds % 60;
                return seconds;
            }
            set
            {
                seconds = value;
                RaisePropertyChanged();
            }
        }
    }
}
