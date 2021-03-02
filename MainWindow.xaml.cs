using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace LogViewExample
{
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
      
        public MainWindow(LogViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
      
    }
}
