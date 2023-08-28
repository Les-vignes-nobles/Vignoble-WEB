using Microsoft.Extensions.DependencyInjection.Extensions;
using VignobleWEB.Core.Application.Repositories;
using VignobleWEB.Core.Application.Tools;
using VignobleWEB.Core.Infrastructure.DataLayers;
using VignobleWEB.Core.Infrastructure.Tools;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Extensions;

public static class ServicesExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        //Permet lors de l'utilisation d'une interface de table en base de données de le lier au DataLayer associé
        services.TryAddScoped<IProductDataLayer, APIProductDataLayer>();

        //Permet lors de l'utilisation d'une interface de repository de le lier à son repository associé
        services.TryAddScoped<IProductRepository, ProductRepository>();

        //Ajout du scope sur les Tools Infrastructure
        services.TryAddScoped<ILogInfrastructure, LogInfrastructure>();

        //Ajout du scope sur les Tools Repository
        services.TryAddScoped<ILogRepository, LogRepository>();

    }
}