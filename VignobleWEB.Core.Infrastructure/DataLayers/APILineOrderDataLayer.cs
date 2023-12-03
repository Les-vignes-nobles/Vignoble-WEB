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
    public class APILineOrderDataLayer : ILineOrderDataLayer
    {
        #region Champs
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogInfrastructure _logInfrastructure;
        #endregion

        #region Constructeur
        public APILineOrderDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory, ILogInfrastructure logInfrastructure)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logInfrastructure = logInfrastructure;
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public async Task<List<LineOrder>> GetLinesOrderByHeaderOrder(Guid headerOrder)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}lineOrderByHeaderOrder/{headerOrder}";
            var req = await client.GetAsync(url);
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            var json = await req.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<LineOrder>>(json) ?? new List<LineOrder>();
        }
        #endregion

        #endregion
    }
}
