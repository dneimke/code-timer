using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;
using CodeTimer.Abstractions;

namespace CodeTimer 
{

    public class CodeTimer : ICodeTimer 
    {

        private string name;
        private List<Marker> markers;
        private long expectedMilliseconds = 0;

        private readonly ILogger logger;
        private readonly IPerformanceTimer timer;
        private readonly ILogFormatter logFormatter;
      
        public CodeTimer(string name)
        {
            this.name = name;
            timer = new PerformanceTimer();
            logFormatter = new LogFormatter();
            markers = new List<Marker>();

            timer.Start();
        }

        public CodeTimer(string name, ILogger logger)
        {
            this.name = name;
            this.logger = logger;
            timer = new PerformanceTimer();
            logFormatter = new LogFormatter(this);
            markers = new List<Marker>();

            timer.Start();
        }

        public CodeTimer(string name, ILogger logger, IPerformanceTimer performanceTimer)
        {
            this.name = name;
            this.logger = logger;
            timer = performanceTimer;
            logFormatter = new LogFormatter(this);
            markers = new List<Marker>();

            timer.Start();
        }

        public CodeTimer(string name, ILogger logger, IPerformanceTimer performanceTimer, ILogFormatter logFormatter)
        {
            this.name = name;
            this.logger = logger;
            timer = performanceTimer;
            this.logFormatter = logFormatter;
            markers = new List<Marker>();

            logFormatter.SetCodeTimer(this);

            timer.Start();
        }

        public long ExpectedMilliseconds { get => expectedMilliseconds; set => expectedMilliseconds = value; }
        public string Name { get => name; set => name = value; }

        public void Complete()
        {
            timer.Stop();

            if(logger != null) {

                var logMessage = this.GetFormattedResult();

                if(this.Success()) {
                    logger.LogInformation(logMessage);
                }else{
                    logger.LogError(logMessage);
                }
            }
        }

        public long GetElapsedMilliseconds()
        {
            return timer.ElapsedMilliseconds;
        }

        public string GetFormattedResult()
        {
            return logFormatter.GetFormattedLogText();
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