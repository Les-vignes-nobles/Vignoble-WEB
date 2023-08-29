using Microsoft.Extensions.Configuration;
using System.Text;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.Token;

namespace VignobleWEB.Core.Infrastructure.Token
{
    public class TokenAPI : ITokenAPI
    {
        #region Champs
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private string token;

        #endregion

        public string ReadTokenAPI()
        {
            return token;
        }

        public void UpdateTokenAPI(string token)
        {
            this.token = token;
        }

        public async Task<string> GetTokenAPI()
        {
            string url = $"{config["ConnectionStrings:UrlAPIConnection"]}/Login";

            string jsonObject = string.Format("{{\"username\":\"{0}\", \"password\":\"{1}\"}}", config["ConnectionStrings:APIUsername"], config["ConnectionStrings:APIPassword"]);

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                    throw new DataLayersException(response.StatusCode.ToString());

                var resp = await response.Content.ReadAsStringAsync();
                UpdateTokenAPI(resp);
                return resp;
            }
        }
    }
}
