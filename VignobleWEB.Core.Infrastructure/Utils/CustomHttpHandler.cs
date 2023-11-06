using System.Net;
using System.Net.Http.Headers;
using VignobleWEB.Core.Infrastructure.Token;
using VignobleWEB.Core.Interfaces.Infrastructure.Services;

namespace VignobleWEB.Core.Infrastructure.Utils;

public class CustomHttpHandler : DelegatingHandler
{
    private readonly IAuthService _authService;

    public CustomHttpHandler(IAuthService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is not (HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)) return response;

        if (TokenManager.Instance.GetToken().Length == 0)
        {
            var user = TokenManager.Instance.GetCredentials().TryGetValue("Username", out var username);
            var pass = TokenManager.Instance.GetCredentials().TryGetValue("Password", out var password);
            if (user && pass)
                await _authService.LogIn(username, password);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Instance.GetToken());
            response = await base.SendAsync(request, cancellationToken);

        }
        else
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Instance.GetToken());
            response = await base.SendAsync(request, cancellationToken);
        }
        return response;
    }
}