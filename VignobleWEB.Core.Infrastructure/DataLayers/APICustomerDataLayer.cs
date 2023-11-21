using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APICustomerDataLayer : ICustomerDataLayer
    {
        #region Champs
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogInfrastructure _logInfrastructure;
        #endregion

        #region Constructeur
        public APICustomerDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory, ILogInfrastructure logInfrastructure)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logInfrastructure = logInfrastructure;
        }
        #endregion

        #region Méthodes publiques

        #region Create
        public async Task<bool> CreateCustomer(Customer customer)
        {
            Customer customerAdd = new Customer
            {
                CustomerSurname = customer.CustomerSurname,
                CustomerName = customer.CustomerName,
                Gender = customer.Gender,  
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address,
                ZipCode = customer.ZipCode,
                Town = customer.Town,
                Country = customer.Country
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(customerAdd), Encoding.UTF8, "application/json");

            using (HttpClient client = _httpClientFactory.CreateClient("Auth"))
            {
                try
                {
                    client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
                    var url = $"{client.BaseAddress}customer";
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        _logInfrastructure.LogInfo($"Erreur lors de la requête POST. Code d'erreur : {response.StatusCode}");
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                    }
                }
                catch (DataLayersException ex)
                {
                    throw;
                }
            }
            return true;
        }
        #endregion

        #region Read (Lecture)
        public async Task<Customer> GetAddress(string mailuser)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}customer/getByNameOrEmail/{mailuser}";
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
