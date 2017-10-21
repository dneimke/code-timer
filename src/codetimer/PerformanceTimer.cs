using System.Diagnostics;
using CodeTimer.Abstractions;

namespace CodeTimer
{
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