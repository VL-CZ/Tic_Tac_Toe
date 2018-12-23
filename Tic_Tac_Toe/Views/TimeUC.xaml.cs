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
using System.Windows.Threading;
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

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            pageVM.Seconds++;
        }
    }
}
