using System.Windows;
using System.Windows.Controls;
using Tic_Tac_Toe.ViewModels;

namespace Tic_Tac_Toe.Views
{
    /// <summary>
    /// Interaction logic for OnePlayerUC.xaml
    /// </summary>
    public partial class OnePlayerUC : UserControl
    {
        private OnePlayerVM pageVM;

        public OnePlayerUC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // initialize pageVM
            pageVM = pageVM ?? (OnePlayerVM)DataContext;

            // get cellID from Tag
            int cellID = int.Parse((sender as Button).Tag.ToString());

            #region User's move
            // user's move
            if (!pageVM.GameBoard.IsEmpty(cellID))
                return;

            pageVM.GameBoard.Place(cellID);

            // Winner?
            char? winner = pageVM.GameBoard.Winner;
            if (winner != null)
            {
                pageVM.GameTimer.StopTimer();
                WinnerTextBlock.Visibility = Visibility.Visible;
            }
            #endregion

            #region AI's move
            // AI's move
            pageVM.GameBot.BestMove();

            // Winner?
            winner = pageVM.GameBoard.Winner;
            if (winner != null)
            {
                pageVM.GameTimer.StopTimer();
                WinnerTextBlock.Visibility = Visibility.Visible;
            }
            #endregion

        }
    }
}