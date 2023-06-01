namespace VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee
{
    public class DataLayersException : Exception
    {
        public DataLayersException() { }
        public DataLayersException(string messageKey) : base(messageKey) { }
    }
}
