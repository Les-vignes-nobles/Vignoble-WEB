using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers;

public class APITransportDataLayer : ITransportDataLayer
{
    #region Champs

    private readonly IOptions<ApiSettings> _config;
    private readonly IHttpClientFactory _httpClientFactory;
    #endregion

    #region Constructeur
    public APITransportDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
    }
    #endregion

    #region Méthodes publiques

    #region Read (Lecture)
    public async Task<List<Transport>> GetAllTransports()
    {
        using var client = _httpClientFactory.CreateClient("Auth");
        client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var url = $"{client.BaseAddress}transport";
        var req = await client.GetAsync(url);
        if (!req.IsSuccessStatusCode)
            throw new DataLayersException(req.StatusCode.ToString());

        var json = await req.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Transport>>(json) ?? new List<Transport>();
    }
    #endregion

    #endregion
}