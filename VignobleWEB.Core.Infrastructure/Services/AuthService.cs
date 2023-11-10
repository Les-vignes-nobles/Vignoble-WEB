using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using VignobleWEB.Core.Infrastructure.Token;
using VignobleWEB.Core.Interfaces.Infrastructure.Services;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.Services;

public class AuthService : IAuthService
{

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<ApiSettings> _options;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options, ILogger<AuthService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
        _logger = logger;
    }


    public async Task LogIn(string email, string password)
    {
        try
        {
            using var http = _httpClientFactory.CreateClient();
            http.BaseAddress = new Uri($"{_options.Value.BaseUrl!}Login");
            var logInfo = new StringContent(JsonConvert.SerializeObject(new LogInfo
            {
                Email = email,
                Password = password
            }), Encoding.Unicode, "application/json");
            var req = await http.PostAsync(http.BaseAddress, logInfo);
            if (req.IsSuccessStatusCode)
            {
                var token = await req.Content.ReadAsStringAsync();
                TokenManager.Instance.SetToken(email, password, token);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }

    }
}