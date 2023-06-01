using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using Microsoft.Extensions.Logging;

namespace VignobleWEB.Core.Infrastructure.Tools
{
    /// <summary>
    /// Code permettant d'écrire des logs
    /// </summary>
    public class LogInfrastructure : ILogInfrastructure
    {
        private readonly ILogger<LogInfrastructure> logger;

        public LogInfrastructure(ILogger<LogInfrastructure> logger)
        {
            this.logger = logger;
        }

        public void LogAvertissement(string message)
        {
            this.logger.LogWarning(message);
        }

        public void LogCritique(string message, Exception exception)
        {
            this.logger.LogCritical(exception, message);
        }

        public void LogErreur(string message, Exception exception)
        {
            this.logger.LogError(exception, message);
        }

        public void LogInfo(string message)
        {
            this.logger.LogInformation(message);
        }
    }
}
