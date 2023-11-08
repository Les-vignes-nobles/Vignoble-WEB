using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        public async Task<User> CreateUser(User user, Customer customer)
        {
            //string jsonObject = string.Format("{{\"username\":\"{0}\", " +
            //    "\"role\":\"{1}\", " +
            //    "\"birthday\":\"{2}\", " +
            //    "\"encryptPassword\":\"{3}\", " +
            //    "\"passwordSalt\":\"{4}\", " +
            //     "}}", user.Email, user.Role, user.BirthDay, user.EncryptPassword, user.PasswordSalt);

            //using (HttpClient client = new HttpClient())
            //{
            //    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            //    HttpResponseMessage response = await client.PostAsync(url, content);

            //    if (!response.IsSuccessStatusCode) { throw new DataLayersException(response.StatusCode.ToString()); }

            //    var resp = await response.Content.ReadAsStringAsync();

            //    return new User();
            //}


            using (var client = _httpClientFactory.CreateClient("Auth"))
            {
                string jsonObject = string.Format("{{\"role\":\"{0}\", " +
                    "\"username\":\"{1}\", " +
                    "\"encryptPassword\":\"{2}\", " +
                    "\"passwordSalt\":\"{3}\", " +
                     "}}", user.Role, user.Email, user.EncryptPassword, user.PasswordSalt);

                client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = $"{client.BaseAddress}user";
                var req = await client.PostAsync(url, new StringContent(jsonObject));
                if (!req.IsSuccessStatusCode)
                    throw new DataLayersException(req.StatusCode.ToString());

            }
            return user;
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
