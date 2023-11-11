using Microsoft.Extensions.Options;
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
        public async Task<bool> CreateUser(User user, Customer customer)
        {
            using (var client = _httpClientFactory.CreateClient("Auth"))
            {
                string jsonObject = string.Format("{{\"username\":\"{0}\", " +
                    "\"email\":\"{1}\", " +
                    "\"birthday\":\"{2}\", " +
                    "\"password\":\"{3}\", " +
                    "\"role\":\"{4}\", " +
                     "}}", user.UserName, user.Email, user.BirthDay, user.EncryptPassword, user.Role);

                client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = $"{client.BaseAddress}user";
                var req = await client.PostAsync(url, new StringContent(jsonObject));
                if (!req.IsSuccessStatusCode)
                    throw new DataLayersException(req.StatusCode.ToString());

            }
            return true;
        }
        #endregion

        #region Read (Lecture)
        #endregion

        #region Update (Modification)
        #endregion

        #region Delete (Suppression)
        #endregion

        #endregion
    }
}
