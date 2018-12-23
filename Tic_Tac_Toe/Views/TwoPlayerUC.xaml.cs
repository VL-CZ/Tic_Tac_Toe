﻿using System;
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
        private TwoPlayerVM pageVM = new TwoPlayerVM();
        public TwoPlayerUC()
        {
            InitializeComponent();
            DataContext = pageVM;
        }



    }
}
