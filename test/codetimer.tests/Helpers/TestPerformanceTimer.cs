

using CodeTimer.Abstractions;

namespace CodeTimer.Tests.Helpers 
{

    public class TestPerformanceTimer : IPerformanceTimer
    {
        private long elapsedMilliseconds;

        public TestPerformanceTimer(long elapsedMilliseconds)
        {
            this.elapsedMilliseconds = elapsedMilliseconds;
        }

        public long ElapsedMilliseconds => elapsedMilliseconds;

        public void Start()
        {
            // do nothing
        }

        public void Stop()
        {
            // do nothing
        }

        public void SetElapsedMilliseconds(long elapsedMilliseconds) 
        {
            this.elapsedMilliseconds = elapsedMilliseconds;
        }
    }
}