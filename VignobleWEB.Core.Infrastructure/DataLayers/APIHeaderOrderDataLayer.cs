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
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIHeaderOrderDataLayer : IHeaderOrderDataLayer
    {
        #region Champs
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Constructeur
        public APIHeaderOrderDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        #endregion

        #region Read (Lecture)
        public async Task<List<HeaderOrder>> RecupererListeEnteteCommandeDunClient(Guid idUser)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}headerOrder/{idUser}";
            var req = await client.GetAsync(url);
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            var json = await req.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<HeaderOrder>>(json) ?? new List<HeaderOrder>();
        }
        #endregion

        #region Update (Modification)
        #endregion

        #region Delete (Suppression)
        #endregion

        #endregion

    }
}
