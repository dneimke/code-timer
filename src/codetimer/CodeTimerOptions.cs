using CodeTimer.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTimer
{
    public class CodeTimerOptions
    {
        public string Name { get; set; }
        public long ExpectedMilliseconds { get; set; }
        public bool Verbose { get; set; }
        public ILogger Logger { get; set; }
        public IPerformanceTimer PerformanceTimer { get; set; }
        public ILogFormatter LogFormatter { get; set; }
    }
}
