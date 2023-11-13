using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VignobleWEB.Core.Application.Repositories;
using VignobleWEB.Core.Application.Tools;
using VignobleWEB.Core.Infrastructure.DataLayers;
using VignobleWEB.Core.Infrastructure.Services;
using VignobleWEB.Core.Infrastructure.Tools;
using VignobleWEB.Core.Infrastructure.Utils;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Services;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Extensions;

public static class ServicesExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {

        services.AddHttpClient();
        services.TryAddTransient<CustomHttpHandler>();
        services.AddHttpClient( "Auth")
            .AddHttpMessageHandler<CustomHttpHandler>();


        services.TryAddScoped<IAuthService, AuthService>();

        services.TryAddScoped<IProductDataLayer, APIProductDataLayer>();
        services.TryAddScoped<ITransportDataLayer, APITransportDataLayer>();
        services.TryAddScoped<ICustomerDataLayer, APICustomerDataLayer>();
        services.TryAddScoped<IAccountDataLayer, APIAccountDataLayer>();
        services.TryAddScoped<IHeaderOrderDataLayer, APIHeaderOrderDataLayer>();

        services.TryAddScoped<IProductRepository, ProductRepository>();
        services.TryAddScoped<ITransportRepository, TransportRepository>();
        services.TryAddScoped<ICustomerRepository, CustomerRepository>();
        services.TryAddScoped<IAccountRepository, AccountRepository>();
        services.TryAddScoped<IHeaderOrderRepository, HeaderOrderRepository>();

        services.TryAddScoped<ILogInfrastructure, LogInfrastructure>();

        services.TryAddScoped<ILogRepository, LogRepository>();
    }
}