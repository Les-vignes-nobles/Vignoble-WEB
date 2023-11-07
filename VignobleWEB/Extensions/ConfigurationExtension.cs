using VignobleWEB.Core.Models;

namespace VignobleWEB.Extensions;

public static class ConfigurationExtension
{
    public static void AddConfiguration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddOptions<ApiSettings>()
            .Bind(configuration.GetSection(ApiSettings.Api));
    }
}