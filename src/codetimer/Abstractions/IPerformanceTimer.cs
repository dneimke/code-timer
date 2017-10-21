
namespace CodeTimer.Abstractions 
{
    public interface IPerformanceTimer
    {
        void Start();
        void Stop();
        long ElapsedMilliseconds {get;}
    }
}