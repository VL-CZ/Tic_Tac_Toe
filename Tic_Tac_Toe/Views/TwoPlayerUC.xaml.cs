using System.Windows;
using System.Windows.Controls;
using Tic_Tac_Toe.ViewModels;

namespace Tic_Tac_Toe.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TwoPlayerUC : UserControl
    {
        private TwoPlayerVM pageVM;

        public TwoPlayerUC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // initialize pageVM
            pageVM = pageVM ?? (TwoPlayerVM)DataContext;

            int id = int.Parse((sender as Button).Tag.ToString());

            pageVM.GameBoard.Place(id);

            // Winner?
            char? winner = pageVM.GameBoard.Winner;
            if (winner != null)
            {
                TimeVM timeVM = (TimeVM)TimeUC.DataContext;
                timeVM.StopTimer();
                WinnerTextBlock.Text = $"Winner: {winner}";
            }
        }
    }
}