using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LogViewExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LogService _logService;
        private LogViewModel _logViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _logService = new LogService();
            _logViewModel = new LogViewModel(_logService);
            var window = new MainWindow(_logViewModel);
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            _logViewModel.Dispose();
            _logService.Dispose();
        }

    }
}
