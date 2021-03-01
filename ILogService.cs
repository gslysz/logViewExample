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
    }
}