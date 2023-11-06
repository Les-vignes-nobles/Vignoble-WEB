namespace VignobleWEB.Core.Interfaces.Infrastructure.Services;

public interface IAuthService
{
    Task LogIn(string email, string password);
}