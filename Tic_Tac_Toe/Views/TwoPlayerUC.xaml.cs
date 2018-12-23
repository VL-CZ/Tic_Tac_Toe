using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tic_Tac_Toe.Models;
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
            // assign pageVM
            if (pageVM == null)
                pageVM = (TwoPlayerVM)DataContext;

            string id = (sender as Button).Tag.ToString();
            pageVM.GameBoard.Place(id);
            if (pageVM.GameBoard.Winner != null)
            {
                MessageBox.Show($"Winner: {pageVM.GameBoard.Winner}");
            }
        }
    }
}
