using HikingTracks.Domain.Interfaces;
using NLog;

namespace HikingTracks.LoggerService;

public class LoggerManager : ILoggerManager
{
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

    public LoggerManager()
    {

    }
    
    public void LogDebug(string message)
    {
        throw new NotImplementedException();
    }

    public void LogError(string message)
    {
        throw new NotImplementedException();
    }

    public void LogInfo(string message)
    {
        throw new NotImplementedException();
    }

    public void LogWarn(string message)
    {
        throw new NotImplementedException();
    }
}
