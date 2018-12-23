using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.ViewModels
{
    class MainWindowVM : BaseVM
    {
        private BaseVM _currentViewModel;
        public BaseVM CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged();
            }
        }

    }
}
