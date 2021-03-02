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
            set
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
            set
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
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public List<LogEntry> Contents { get; set; }

        public virtual bool HasChildren => Contents != null && Contents.Count > 0;
    }

    /// <summary>
    /// Log entry that contains child log enti
    /// </summary>
    public class CollapsibleLogEntry : LogEntry
    {
        
    }
}