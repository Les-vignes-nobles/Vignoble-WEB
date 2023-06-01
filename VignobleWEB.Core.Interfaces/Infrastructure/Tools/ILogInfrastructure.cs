namespace VignobleWEB.Core.Interfaces.Infrastructure.Tools
{
    /// <summary>
    /// Interface permettant d'écrire des logs de l'application
    /// </summary>
    public interface ILogInfrastructure
    {
        void LogInfo(string message);
        void LogAvertissement(string message);
        void LogErreur(string message, Exception exception);
        void LogCritique(string message, Exception exception);
    }
}
