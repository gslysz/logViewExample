using System;
using System.Collections.Generic;
using System.IO;

namespace LogViewExample.Testing
{
    public class TestLogService : LogService
    {
        public TestLogService()
        {
            _logEntries = new List<LogEntry>();

            using (StreamReader reader = new StreamReader("..\\..\\sampleMessages.txt"))
            {
                int index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var logEntry = LogEntry.Create(false, index++, DateTime.Now);
                    logEntry.Message = line;

                    _logEntries.Add(logEntry);
                }




            }
        }

    }
}