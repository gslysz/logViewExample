using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace LogViewExample
{
    /// <summary>
    /// Represents a single log entry
    /// </summary>
    public class LogEntry : ObservableObject
    {
        private string message;


        internal LogEntry(int index, DateTime dateTime)
        {
            DateTime = dateTime;
            Index = index;
        }


        /// <summary>
        /// The date/time of the log entry
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Log entry identifier
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Log message
        /// </summary>
        public string Message
        {
            get => message;
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }

        public List<LogEntry> ChildLogEntries { get; set; }

        public bool HasChildren => ChildLogEntries != null && ChildLogEntries.Count > 0;


        public static LogEntry Create(bool hasChildLogEntries,  int index, DateTime dateTime)
        {
            if (hasChildLogEntries)
                return new CollapsibleLogEntry(index, dateTime);
            else
                return new LogEntry(index, dateTime);
        }

    }

    /// <summary>
    /// Log entry that contains child log entries
    /// </summary>
    public class CollapsibleLogEntry : LogEntry
    {
        internal CollapsibleLogEntry(int index, DateTime dateTime) : base(index, dateTime)
        {
        }
    }

}