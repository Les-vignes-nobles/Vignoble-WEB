using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIAccountDataLayer : IAccountDataLayer
    {
        #region Champs
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogInfrastructure _logInfrastructure;
        #endregion

        #region Constructeur
        public APIAccountDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory, ILogInfrastructure logInfo)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logInfrastructure = logInfo;
        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public async Task<bool> CreateUser(User user, Customer customer)
        {
            
            string jsonObject = string.Format("{{\"id\":\"{0}\", " +
                "\"username\":\"{1}\", " +
                "\"email\":\"{2}\", " +
                "\"birthDay\":\"{3}\", " +
                "\"password\":\"{4}\", " +
                "\"role\":\"{5}\"," +
                "\"surname\":\"{6}\", " +
                "\"name\":\"{7}\", " +
                "\"gender\":\"{8}\", " +
                "\"phoneNumber\":\"{9}\", " +
                "\"address\":\"{10}\", " +
                "\"zipCode\":\"{11}\", " +
                "\"town\":\"{12}\", " +
                "\"country\":\"{13}\" " +
                "}}", user.Id, user.Email, user.Email, user.BirthDay, user.Password, user.Role, 
                customer.CustomerSurname, customer.CustomerName, customer.Gender, customer.PhoneNumber,
                customer.Address, customer.ZipCode, customer.Town, customer.Country);

            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            using (HttpClient client = _httpClientFactory.CreateClient("Auth"))
            {
                try
                {
                    client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
                    var url = $"{client.BaseAddress}user";
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
        #endregion

        #region Update (Modification)
        #endregion

        #region Delete (Suppression)
        public async Task<bool> DeleteUser(string guidUser)
        {
            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"{client.BaseAddress}user/{guidUser}";
            var req = await client.DeleteAsync(url);
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());

            return true;
        }
        #endregion

        #endregion
    }
}
