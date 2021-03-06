using System;
using System.Text;
using CodeTimer.Abstractions;

namespace CodeTimer
{
    /// <summary>
    /// Formatter used to output the state of a code timer to a string format.  This is the 
    /// default implementation that is used to write to Logs.
    /// </summary>
    internal class LogFormatter : ILogFormatter
    {
        private ICodeTimer codeTimer;

        public LogFormatter()
        {
        }

        public LogFormatter(ICodeTimer codeTimer)
        {
            this.codeTimer = codeTimer;
        }

        public void SetCodeTimer(ICodeTimer codeTimer)
        {
            this.codeTimer = codeTimer;
        }

        public string GetFormattedLogText(bool verbose)
        {
            if(codeTimer != null)
            {
                var logMessage = (verbose) ? GetVerboseFormat() : GetNonVerboseFormat();
                return logMessage;
            }

            return "";
        }

        private string GetVerboseFormat()
        {
            var jobName = string.IsNullOrEmpty(codeTimer.Name) ? "CodeTimer" : $"{codeTimer.Name} timer";
            var timeTaken = codeTimer.GetElapsedMilliseconds();
            var result = codeTimer.Success() ? "succeeded" : "failed";

            var formattedHeader = (codeTimer.ExpectedMilliseconds == 0) ?
                $"{jobName} {result}.  Ran for {timeTaken}ms." :
                $"{jobName} {result}.  Ran for {timeTaken}ms, expected {codeTimer.ExpectedMilliseconds}ms.";

            var sb = new StringBuilder();

            sb.AppendLine(formattedHeader);

            foreach (var marker in codeTimer.GetMarkers())
            {
                sb.AppendLine($" - {marker.Name}: {marker.Ticks}ms");
            }
            var logMessage = sb.ToString();

            return logMessage;
        }

        private string GetNonVerboseFormat()
        {
            var jobName = string.IsNullOrEmpty(codeTimer.Name) ? "CodeTimer" : $"{codeTimer.Name}";
            var timeTaken = codeTimer.GetElapsedMilliseconds();
            var expected = codeTimer.ExpectedMilliseconds;
            var result = codeTimer.Success() ? "succeeded" : "failed";

            var sb = new StringBuilder();

            sb.Append($"{jobName},{expected},{timeTaken},{result},");

            foreach (var marker in codeTimer.GetMarkers())
            {
                sb.Append($"{marker.Ticks},");
            }
            var logMessage = sb.ToString();
            logMessage = logMessage.TrimEnd(new char[] { ',' });

            return logMessage;
        }
    }
}