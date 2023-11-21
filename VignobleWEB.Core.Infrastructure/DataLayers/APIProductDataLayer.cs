using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers;

public class APIProductDataLayer : IProductDataLayer
{
    #region Champs

    private readonly IOptions<ApiSettings> _config;
    private readonly IHttpClientFactory _httpClientFactory;
    #endregion

    #region Constructeur
    public APIProductDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
    }
    #endregion

    #region Méthodes publiques

    #region Read (Lecture)
    public async Task<List<Product>> GetAllProducts()
    {
        using var client = _httpClientFactory.CreateClient("Auth");
        client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var url = $"{client.BaseAddress}product";
        var req = await client.GetAsync(url);
        if (!req.IsSuccessStatusCode)
            throw new DataLayersException(req.StatusCode.ToString());

        var json = await req.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
    }
    #endregion

    #endregion

}