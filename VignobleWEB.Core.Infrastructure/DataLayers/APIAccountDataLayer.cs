using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIAccountDataLayer : IAccountDataLayer
    {
        #region Champs
        private readonly IOptions<ApiSettings> _config;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Constructeur
        public APIAccountDataLayer(IOptions<ApiSettings> config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public async Task<bool> CreateUser(User user)
        {
            
            string jsonObject = string.Format("{{\"id\":\"{0}\", " +
                "\"username\":\"{1}\", " +
                "\"email\":\"{2}\", " +
                "\"birthDay\":\"{3}\", " +
                "\"password\":\"{4}\", " +
                "\"role\":\"{5}\", " +
                    "}}", user.Id, user.Email, user.Email, user.BirthDay, user.Password, user.Role);

            using var client = _httpClientFactory.CreateClient("Auth");
            client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{client.BaseAddress}user";
            var req = await client.PostAsync(url, new StringContent(jsonObject));
            if (!req.IsSuccessStatusCode)
                throw new DataLayersException(req.StatusCode.ToString());
            
            return true;
        }
        #endregion

        #region Read (Lecture)
        #endregion

        #region Update (Modification)
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

        #region Delete (Suppression)
        #endregion

        #endregion
    }
}
