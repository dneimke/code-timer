
namespace CodeTimer.Abstractions 
{
    public interface ILogFormatter
    {
        void SetCodeTimer(ICodeTimer codeTimer);
        string GetFormattedLogText();
    }
}