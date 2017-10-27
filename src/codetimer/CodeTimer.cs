using CodeTimer.Abstractions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CodeTimer
{

    public class CodeTimer : ICodeTimer 
    {

        private string name = "";
        private long expectedMilliseconds = 0;
        private bool verbose = false;
        private ILogger logger = null;
        private IPerformanceTimer timer = null;
        private ILogFormatter logFormatter = null;

        private List<Marker> markers = new List<Marker>();

        /// <summary>
        /// Constructs a new CodeTimer with the default Timer and Formatter
        /// </summary>
        /// <param name="name">The name to associate with the timer</param>
        public CodeTimer(string name)
        {
            this.name = name;
            timer = new PerformanceTimer();
            logFormatter = new LogFormatter();

            timer.Start();
        }

        /// <summary>
        /// Constructs a new CodeTimer with logging enabled using the default Timer and Formatter
        /// </summary>
        /// <param name="name">The name to associate with the timer</param>
        /// <param name="logger">The ILogger instance to be used for logging</param>
        public CodeTimer(string name, ILogger logger)
        {
            this.name = name;
            this.logger = logger;
            timer = new PerformanceTimer();
            logFormatter = new LogFormatter(this);

            timer.Start();
        }

        /// <summary>
        /// Constructs a new CodeTimer using the options which are passed in.
        /// </summary>
        /// <param name="options">The options to use to configure the CodeTimer</param>
        /// <remarks>
        /// Defaults are used for Timer and LogFormatter if null values are passed in.
        /// </remarks>
        public CodeTimer(CodeTimerOptions options)
        {
            name = options.Name ?? "CodeTimer";
            expectedMilliseconds = options.ExpectedMilliseconds;
            verbose = options.Verbose;
            timer = options.PerformanceTimer ?? new PerformanceTimer();
            logFormatter = options.LogFormatter ?? new LogFormatter(this);

            timer.Start();
        }

        public long ExpectedMilliseconds { get => expectedMilliseconds; set => expectedMilliseconds = value; }

        public string Name { get => name; set => name = value; }
        public bool Verbose { get => verbose; set => verbose = value; }

        public void Complete()
        {
            timer.Stop();

            if(logger != null) {

                var logMessage = this.GetFormattedResult();

                if(this.Success()) {
                    logger.LogInformation(logMessage);
                }else{
                    logger.LogWarning(logMessage);
                }
            }
        }

        public long GetElapsedMilliseconds()
        {
            return timer.ElapsedMilliseconds;
        }

        public string GetFormattedResult()
        {
            return logFormatter.GetFormattedLogText(Verbose);
        }

        public IList<Marker> GetMarkers()
        {
            return markers;
        }

        public void Mark()
        {
            markers.Add(new Marker(timer.ElapsedMilliseconds));
        }

        public void Mark(string markerName)
        {
            markers.Add(new Marker(timer.ElapsedMilliseconds, markerName));
        }

        public bool Success()
        {
            if(ExpectedMilliseconds > 0)
            {
                return GetElapsedMilliseconds() <= ExpectedMilliseconds;
            }

            return true;
        }
    }
}