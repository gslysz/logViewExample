using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogViewExample.Testing
{
    [TestClass]
    public class LogViewModelTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            ILogService logService = new TestLogService();
            LogViewModel viewModel = new LogViewModel(logService);

            //Should be capped at 10000
            Assert.AreEqual(10000, viewModel.FilteredLogEntries.Count);

            //Test the search functionality
            viewModel.SearchText = "qui qui";
            Assert.AreEqual(127, viewModel.FilteredLogEntries.Count);

            //Clear the search text
            viewModel.ClearSearchCommand.Execute(null);

            Assert.AreEqual(10000, viewModel.FilteredLogEntries.Count);
        }
    }
}
