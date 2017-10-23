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

        public string GetFormattedLogText()
        {
            if(codeTimer != null)
            {
                var jobName = string.IsNullOrEmpty(codeTimer.Name) ? "CodeTimer" : $"{codeTimer.Name} timer";
                var timeTaken = codeTimer.GetElapsedMilliseconds();
                var result = codeTimer.Success() ? "succeeded" : "failed";

                var formattedHeader = (codeTimer.ExpectedMilliseconds == 0) ?
                    $"{jobName} {result}.  Ran for {timeTaken}ms." :
                    $"{jobName} {result}.  Ran for {timeTaken}ms, expected {codeTimer.ExpectedMilliseconds}ms.";

                var sb = new StringBuilder() ;

                sb.AppendLine(formattedHeader);

                foreach(var marker in codeTimer.GetMarkers()) {
                    sb.AppendLine($" - {marker.Name}: {marker.Ticks}ms");
                }
                var logMessage = sb.ToString();

                return logMessage;
            }

            return "";
        }
    }
}