using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CodeTimer {

    public class CodeTimer : ICodeTimer {

        private readonly string name;
        private readonly ILogger logger;
        private readonly Stopwatch timer;
        private List<Segment> segments;

        public CodeTimer(string name)
        {
            this.name = name;
            this.segments = new List<Segment>();
            this.timer = new Stopwatch();
            timer.Start();
        }

        public CodeTimer(string name, ILogger logger)
        {
            this.name = name;
            this.logger = logger;
            this.segments = new List<Segment>();
            this.timer = new Stopwatch();
            timer.Start();
        }

        public long ExpectedMilliseconds { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Complete()
        {
            this.timer.Stop();

            if(this.logger != null) {

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
            return this.timer.ElapsedMilliseconds;
        }

        public string GetFormattedResult()
        {
            var jobName = string.IsNullOrEmpty(this.name) ? "CodeTimer Job" : this.name;
            var timeTaken = this.GetElapsedMilliseconds();
            var result = this.Success() ? "succeeded" : "failed";

            var formattedHeader = $"{jobName} ran for {timeTaken}ms and {result}";

            var sb = new StringBuilder() ;

            sb.AppendLine(formattedHeader);

            foreach(var segment in this.segments) {
                sb.AppendLine($"{segment.Name} timings - {segment.Ticks}");
            }
            var logMessage = sb.ToString();

            return logMessage;
        }

        public IEnumerable<Segment> GetSegments()
        {
            return this.segments;
        }

        public void Mark()
        {
            this.segments.Add(new Segment(this.timer.ElapsedMilliseconds));
        }

        public void Mark(string segmentName)
        {
            this.segments.Add(new Segment(this.timer.ElapsedMilliseconds, segmentName));
        }

        public bool Success()
        {
            if(this.ExpectedMilliseconds > 0)
            {
                return this.GetElapsedMilliseconds() <= this.ExpectedMilliseconds;
            }

            return true;
        }
    }
}