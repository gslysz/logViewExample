using System;
using System.Collections.Generic;

namespace LogViewExample
{
    /// <summary>
    /// Provides log entry data
    /// </summary>
    public interface ILogService
    {

        event EventHandler<LogEntry> ReceivedLogEntry;

        List<LogEntry> GetLogEntries();

        /// <summary>
        /// Gets the most recent n-number of log entries
        /// </summary>
        /// <param name="numEntries"></param>
        /// <returns></returns>
        List<LogEntry> GetRecentLogEntries(int numEntries = 1000);
    }
}