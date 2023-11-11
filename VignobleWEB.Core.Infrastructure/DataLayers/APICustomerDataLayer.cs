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
    public class APICustomerDataLayer : ICustomerDataLayer
    {
        #region Champs
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Constructeur
        public APICustomerDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Méthodes publiques

        #region Create
        public async Task<bool> CreateCustomer(Customer customer)
        {
            string jsonObject = string.Format("{{\"customerSurname\":\"{0}\", " +
                    "\"customerName\":\"{1}\", " +
                    "\"phoneNumber\":\"{2}\", " +
                    "\"email\":\"{3}\", " +
                    "\"address\":\"{4}\", " +
                    "\"zipCode\":\"{5}\", " +
                    "\"town\":\"{6}\", " +
                    "\"country\":\"{7}\", " +
                     "}}", customer.CustomerSurname, customer.CustomerName, customer.PhoneNumber, customer.Email, customer.Address, customer.ZipCode, customer.Town, customer.Country);

            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{client.BaseAddress}customer/";
            var req = await client.PostAsync(url, new StringContent(jsonObject));
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            return true;
        }
        #endregion

        #region Read (Lecture)
        public async Task<Customer> GetAdress(int idUser)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}customer/{idUser}";
            var req = await client.GetAsync(url);
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            var json = await req.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Customer>(json) ?? new Customer();
        }
        #endregion

        #endregion
    }
}
