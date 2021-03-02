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
    /// <summary>
    /// Main viewModel for log entries
    /// </summary>
    public class LogViewModel : ObservableObject, IDisposable
    {
        /// <summary>
        /// Data service that provides the log entry data
        /// </summary>
        private ILogService _logService;
        private List<LogEntry> _logEntries;
        private string _searchText;
        private ObservableCollection<LogEntry> filteredLogEntries;
        private object _logEntryLockObject = new object();
        
        /// <summary>
        /// Maximum log size. If the log exceeds this value, oldest items are removed from the log
        /// </summary>
        private const int MaxLogSize = 100000;

        #region Constructors

        public LogViewModel(ILogService logService)
        {
            _logService = logService;

            //Get the 10,000 most recent entries
            _logEntries = _logService.GetRecentLogEntries(10000);

            UpdateFilteredLogEntries();

            _logService.ReceivedLogEntry += LogService_ReceivedLogEntry;

            ClearSearchCommand = new RelayCommand(ClearSearch);
        }

        
        #endregion


        #region Properties

        /// <summary>
        /// Clears the search field
        /// </summary>
        public ICommand ClearSearchCommand { get; set; }

        /// <summary>
        /// Log entries
        /// </summary>
        public ObservableCollection<LogEntry> FilteredLogEntries
        {
            get => filteredLogEntries;
            set
            {
                filteredLogEntries = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Search text used for filtering log entries (based on message)
        /// </summary>
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

        private void UpdateFilteredLogEntries()
        {
            lock (_logEntryLockObject)
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                    FilteredLogEntries = new ObservableCollection<LogEntry>(_logEntries);
                else
                    FilteredLogEntries = new ObservableCollection<LogEntry>(_logEntries.Where(p => p.Message.Contains(_searchText)));
            }

        }


        private void LogService_ReceivedLogEntry(object sender, LogEntry e)
        {

            bool shouldFilter = !string.IsNullOrWhiteSpace(SearchText);

            lock (_logEntryLockObject)
            {
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

                    if (FilteredLogEntries.Count > MaxLogSize)
                        FilteredLogEntries.RemoveAt(0);

                }));
            }

            
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