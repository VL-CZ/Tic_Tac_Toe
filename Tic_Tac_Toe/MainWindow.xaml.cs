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
using Tic_Tac_Toe.ViewModels;

namespace Tic_Tac_Toe
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM windowVM = new MainWindowVM();

        public MainWindow()
        {
            InitializeComponent();
            windowVM.CurrentViewModel = new StartPageVM();
            DataContext = windowVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string vmName = ((Button)sender).Tag.ToString();
            windowVM.ShowViewModel(vmName);
        }

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close(); 
        }
    }
}
