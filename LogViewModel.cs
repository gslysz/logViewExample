using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Windows;

namespace LogViewExample
{
    public class LogViewModel : IDisposable
    {
        /// <summary>
        /// Data service that provides the log entry data
        /// </summary>
        private ILogService _logService;

        private const int MaxLogSize = 1000000;

        #region Constructors

        public LogViewModel(ILogService logService)
        {
            _logService = logService;

            List<LogEntry> initialLogEntries = _logService.GetRecentLogEntries();
            
            LogEntries = new ObservableCollection<LogEntry>(initialLogEntries);

            _logService.ReceivedLogEntry += LogService_ReceivedLogEntry;
           
            
        }

        #endregion


        #region Properties
        public ObservableCollection<LogEntry> LogEntries { get; set; }

        #endregion


        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion


        #region Private Methods
        private void LogService_ReceivedLogEntry(object sender, LogEntry e)
        {
            Application current = Application.Current;
            if (current == null)
                return;

            current.Dispatcher.BeginInvoke((Action)(() =>
            {

                if (LogEntries.Count > MaxLogSize)
                    LogEntries.RemoveAt(0);

                LogEntries.Add(e);
            }));
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            _logService.ReceivedLogEntry -= LogService_ReceivedLogEntry;
        }

        #endregion



    }
}