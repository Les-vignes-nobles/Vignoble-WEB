using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;
using VignobleWEB.Core.Models.Interne;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIHeaderOrderDataLayer : IHeaderOrderDataLayer
    {
        #region Champs
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogInfrastructure _logInfrastructure;
        #endregion

        #region Constructeur
        public APIHeaderOrderDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory, ILogInfrastructure logInfrastructure)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logInfrastructure = logInfrastructure;
        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public async Task<bool> CreateOrder(CreateOrderDto createOrderDto)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(createOrderDto), Encoding.UTF8, "application/json");

            using (HttpClient client = _httpClientFactory.CreateClient("Auth"))
            {
                client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
                var url = $"{client.BaseAddress}headerOrder";
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    return true;
                }
                else
                {
                    _logInfrastructure.LogInfo($"Erreur lors de la requête POST. Code d'erreur : {response.StatusCode}");
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    return false;
                }
            }
        }
        #endregion

        #region Read (Lecture)
        public async Task<List<HeaderOrder>> GetListHeaderOrderByCustomer(Guid idUser)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}headerOrderByCustomerId/{idUser}";
            var req = await client.GetAsync(url);
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            var json = await req.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<HeaderOrder>>(json) ?? new List<HeaderOrder>();
        }

        public async Task<HeaderOrder> GetHeaderOrderById(Guid idHeaderOrder)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}headerOrder/{idHeaderOrder}";
            var req = await client.GetAsync(url);
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            var json = await req.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HeaderOrder>(json) ?? new HeaderOrder();
        }
        #endregion

        #region Update (Modification)
        #endregion

        #region Delete (Suppression)
        #endregion

        #endregion

    }
}
