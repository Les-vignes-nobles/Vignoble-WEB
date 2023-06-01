namespace VignobleWEB.Core.Application.RepositoriesException
{
    public class RepositoryException : Exception
    {
        public RepositoryException() { }

        public RepositoryException(string messageKey) : base(messageKey) { }
    }
}
