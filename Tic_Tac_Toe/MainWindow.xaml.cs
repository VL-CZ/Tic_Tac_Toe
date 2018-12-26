using System.Windows;
using System.Windows.Controls;
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
            if(vmName == nameof(StartPageVM))
            {
                QuitGameButton.Visibility = Visibility.Collapsed;
                GameTextBlock.Visibility = Visibility.Visible;
                P1Button.Visibility = Visibility.Visible;
                P2Button.Visibility = Visibility.Visible;
            }
            else
            {
                QuitGameButton.Visibility = Visibility.Visible;
                GameTextBlock.Visibility = Visibility.Collapsed;
                P1Button.Visibility = Visibility.Collapsed;
                P2Button.Visibility = Visibility.Collapsed;
            }
        }

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
    }
}