using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.Token;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.Token
{
    public class TokenAPI : ITokenAPI
    {
        #region Champs
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private string Token = null;

        #endregion

        public TokenAPI() { }

        public string readTokenAPI()
        {
            return Token;
        }

        public void updateTokenAPI(string token)
        {
            Token = token;
        }

        public async Task getTokenAPI()
        {
            string url = $"{config["ConnectionStrings:UrlAPIConnection"]}/Login";

            string jsonObject = string.Format("{{\"username\":\"{0}\", \"password\":\"{1}\"}}", config["ConnectionStrings:APIUsername"], config["ConnectionStrings:APIPassword"]);

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode) { throw new DataLayersException(response.StatusCode.ToString()); }

                string json = response.Content.ReadAsStringAsync().Result;

                //string token = JsonConvert.DeserializeObject<string>(json);
                string token = await response.Content.ReadAsStringAsync();

                updateTokenAPI(token);

            }
        }
    }
}
