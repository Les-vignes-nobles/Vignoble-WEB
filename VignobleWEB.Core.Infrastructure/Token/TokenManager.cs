namespace VignobleWEB.Core.Infrastructure.Token;

public sealed class TokenManager
{
    private static TokenManager _instance;
    private static readonly object Lock = new object();

    private string? token;

    //TODO Implémenter RefreshToken API
    private string? userName = "User1";
    private string? password = "Password1";


    private TokenManager()
    {
    }

    public static TokenManager Instance
    {
        get
        {
            if (_instance is not null) return _instance;

            lock (Lock)
            {
                return _instance ??= new TokenManager();
            }
        }
    }

    public string GetToken() => token ??= string.Empty;

    public Dictionary<string, string> GetCredentials()
    {
        return new Dictionary<string, string>
        {
            { "Username", userName },
            { "Password", password }
        };
    }

    public bool SetToken(string userName, string password, string token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            this.userName = userName;
            this.password = password;
            this.token = token;
            return true;
        }

        return false;
    }


}