using System.Diagnostics;
using CodeTimer.Abstractions;

namespace CodeTimer
{
    /// <summary>
    /// A wrapper for the Stopwatch class which simplifies testing the CodeTimer system
    /// </summary>
    public class PerformanceTimer : IPerformanceTimer
    {
        private readonly Stopwatch timer;
        public PerformanceTimer()
        {
            timer = new Stopwatch();
        }

        public long ElapsedMilliseconds => timer.ElapsedMilliseconds;

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}