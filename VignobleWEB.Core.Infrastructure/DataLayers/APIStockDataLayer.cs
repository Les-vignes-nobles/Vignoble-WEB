using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIStockDataLayer : IStockDataLayer
    {
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogInfrastructure _logInfrastructure;

        public APIStockDataLayer(ILogInfrastructure logInfrastructure, IHttpClientFactory httpClientFactory, IOptions<ApiSettings> config)
        {
            _logInfrastructure = logInfrastructure;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }
        #region Read (Lecture)
        public async Task<Stock> GetStockById(Guid id)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}stock/{id}";
            var req = await client.GetAsync(url);
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            var json = await req.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Stock>(json) ?? new Stock();
        }
        #endregion
    }
}
