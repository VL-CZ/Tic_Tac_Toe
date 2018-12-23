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
        /// <summary>
        /// current viewmodel of main window
        /// </summary>
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

        /// <summary>
        /// show viewmodel with VM name
        /// </summary>
        /// <param name="VMName"></param>
        public void ShowViewModel(string VMName)
        {
            BaseVM viewModel = null;
            switch (VMName)
            {
                case nameof(OnePlayerVM):
                    viewModel = new OnePlayerVM();
                    break;
                case nameof(TwoPlayerVM):
                    viewModel = new TwoPlayerVM();
                    break;
                case nameof(StartPageVM):
                    viewModel = new StartPageVM();
                    break;
                case nameof(EndPageVM):
                    viewModel = new EndPageVM();
                    break;
                    
            }
            CurrentViewModel = viewModel;
        }

    }
}
