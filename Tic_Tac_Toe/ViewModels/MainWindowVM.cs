﻿namespace Tic_Tac_Toe.ViewModels
{
    internal class MainWindowVM : BaseVM
    {
        private BaseVM currentViewModel;

        /// <summary>
        /// current viewmodel of main window
        /// </summary>
        public BaseVM CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                currentViewModel = value;
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
            }
            CurrentViewModel = viewModel;
        }
    }
}