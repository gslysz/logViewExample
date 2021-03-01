using System;
using System.Collections.Generic;

namespace LogViewExample
{
    /// <summary>
    /// Represents a single log entry
    /// </summary>
    public class LogEntry : PropertyChangedBase
    {
        private DateTime _dateTime;
        private int _index;
        private string message;

        /// <summary>
        /// The date/time of the log entry
        /// </summary>
        public DateTime DateTime
        {
            get => _dateTime;
            internal set
            {
                _dateTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Log entry identifier
        /// </summary>
        public int Index
        {
            get => _index;
            internal set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Log message
        /// </summary>
        public string Message
        {
            get => message;
            internal set
            {
                message = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Log entry that contains child log enti
    /// </summary>
    public class CollapsibleLogEntry : LogEntry
    {
        public List<LogEntry> Contents { get; set; }
    }
}