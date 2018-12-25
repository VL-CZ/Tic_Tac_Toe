using System.Windows.Controls;
using Tic_Tac_Toe.ViewModels;

namespace Tic_Tac_Toe.Views
{
    /// <summary>
    /// Interaction logic for TimeUC.xaml
    /// </summary>
    public partial class TimeUC : UserControl
    {
        private TimeVM pageVM = new TimeVM();

        public TimeUC()
        {
            InitializeComponent();

            DataContext = pageVM;
        }
    }
}