namespace LogViewExample
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System;

    /// <inheritdoc/>
    public class LogService : ILogService, IDisposable
    {
        private Timer _timer;
        private Random _random;
        private int _maxWord;
        private int _index;
        private string _testData = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        private List<string> _words;
        private List<LogEntry> _logEntries = new List<LogEntry>();

        #region Constructors

        public LogService()
        {
            _random = new Random();
            _words = _testData.Split(' ').ToList();
            _maxWord = _words.Count - 1;

            _timer = new Timer(x => AddRandomEntry(), null, 1000, 10);

            Enumerable.Range(0, 200000)
               .ToList()
               .ForEach(x => _logEntries.Add(GetRandomEntry()));
        }

        #endregion


        #region Events

        public event EventHandler<LogEntry> ReceivedLogEntry;

        #endregion


        #region Public Methods

        public List<LogEntry> GetLogEntries()
        {
            return new List<LogEntry>(_logEntries);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion


        #region Private Methods

        private void AddRandomEntry()
        {
            LogEntry randomEntry = GetRandomEntry();
            ReceivedLogEntry?.Invoke(this, randomEntry);
        }

        private LogEntry GetRandomEntry()
        {
            if (_random.Next(1, 10) > 1)
            {
                return new LogEntry()
                {
                    Index = _index++,
                    DateTime = DateTime.Now,
                    Message = string.Join(" ", Enumerable.Range(5, _random.Next(10, 50))
                        .Select(x => _words[_random.Next(0, _maxWord)])),
                };
            }

            return new CollapsibleLogEntry()
            {
                Index = _index++,
                DateTime = DateTime.Now,
                Message = string.Join(" ", Enumerable.Range(5, _random.Next(10, 50))
                    .Select(x => _words[_random.Next(0, _maxWord)])),
                Contents = Enumerable.Range(5, _random.Next(5, 10))
                    .Select(i => GetRandomEntry())
                    .ToList()
            };

        }

        protected void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            _timer.Dispose();
            _timer = null;
        }


        #endregion

    }
}