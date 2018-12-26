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

            int id = int.Parse((sender as Button).Tag.ToString());

            // user's move
            pageVM.GameBoard.Place(id);

            // Winner?
            char? winner = pageVM.GameBoard.Winner;
            if (winner != null)
            {
                pageVM.GameTimer.StopTimer();
                WinnerTextBlock.Visibility = Visibility.Visible;
            }

            // AI's move
            pageVM.GameBot.BestMove();
            if (winner != null)
            {
                pageVM.GameTimer.StopTimer();
                WinnerTextBlock.Visibility = Visibility.Visible;
            }

        }
    }
}