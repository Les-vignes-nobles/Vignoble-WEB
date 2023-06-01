using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Core.Application.Tools
{
    /// <summary>
    /// Permet de faire le lien vers le log Infrastructure
    /// </summary>
    public class LogRepository : ILogRepository
    {
        private readonly ILogInfrastructure _log;

        public LogRepository(ILogInfrastructure logToolsInfrastructure)
        {
            _log = logToolsInfrastructure;
        }

        public void LogAvertissement(string message)
        {
            _log.LogAvertissement(message);
        }

        public void LogCritique(string message, Exception exception)
        {
            _log.LogCritique(message, exception);
        }

        public void LogErreur(string message, Exception exception)
        {
            _log.LogErreur(message, exception);
        }

        public void LogInfo(string message)
        {
            _log.LogInfo(message);
        }
    }
}
