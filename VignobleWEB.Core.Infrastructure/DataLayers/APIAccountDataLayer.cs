using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Token;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIAccountDataLayer : IAccountDataLayer
    {
        #region Champs
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private readonly ITokenAPI _tokenAPI;
        #endregion

        #region Constructeur
        public APIAccountDataLayer(ITokenAPI tokenAPI) 
        { 
            _tokenAPI = tokenAPI;
        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public async Task<User> CreateUser(User user, Customer customer)
        {
            string url = $"{config["ConnectionStrings:UrlAPIConnection"]}/User/AddUser";

            string jsonObject = string.Format("{{\"email\":\"{0}\", " +
                "\"role\":\"{1}\", " +
                "\"birthday\":\"{2}\", " +
                "\"EncryptPassword\":\"{3}\", " +
                "\"PasswordSalt\":\"{4}\", " +
                 "}}", user.Email, user.Role, user.BirthDay, user.EncryptPassword, user.PasswordSalt);

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode) { throw new DataLayersException(response.StatusCode.ToString()); }

                var resp = await response.Content.ReadAsStringAsync();

                return new User();
            }
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
