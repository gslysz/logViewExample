using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace LogViewExample
{
    public class LogViewModel : ObservableObject, IDisposable
    {
        /// <summary>
        /// Data service that provides the log entry data
        /// </summary>
        private ILogService _logService;
        private List<LogEntry> _logEntries;
        private string _searchText;
        private ObservableCollection<LogEntry> filteredLogEntries;
        private const int MaxLogSize = 1000000;

        #region Constructors

        public LogViewModel(ILogService logService)
        {
            _logService = logService;

            _logEntries = _logService.GetRecentLogEntries();

            UpdateFilteredLogEntries();

            _logService.ReceivedLogEntry += LogService_ReceivedLogEntry;

            ClearSearchCommand = new RelayCommand(ClearSearch);
        }

        
        #endregion


        #region Properties

        public ICommand ClearSearchCommand { get; set; }

        public ObservableCollection<LogEntry> LogEntries { get; set; }

        public ObservableCollection<LogEntry> FilteredLogEntries
        {
            get => filteredLogEntries;
            set
            {
                filteredLogEntries = value;
                RaisePropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;

                UpdateFilteredLogEntries();

                RaisePropertyChanged();
            }
        }

        private void UpdateFilteredLogEntries()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                FilteredLogEntries = new ObservableCollection<LogEntry>(_logEntries);
            else
                FilteredLogEntries = new ObservableCollection<LogEntry>(_logEntries.Where(p => p.Message.Contains(_searchText)));

        }

        #endregion


        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion


        #region Private Methods

        private void ClearSearch()
        {
            SearchText = "";

        }

        private void LogService_ReceivedLogEntry(object sender, LogEntry e)
        {

            bool shouldFilter = !string.IsNullOrWhiteSpace(SearchText);

            _logEntries.Add(e);

            if (_logEntries.Count > MaxLogSize)
                _logEntries.RemoveAt(0);

            Application current = Application.Current;
            if (current == null)
                return;

            current.Dispatcher.BeginInvoke((Action)(() =>
            {
                bool shouldAdd = true;

                if (shouldFilter)
                    shouldAdd = e.Message.Contains(SearchText);

                if (shouldAdd)
                    FilteredLogEntries.Add(e);
                
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