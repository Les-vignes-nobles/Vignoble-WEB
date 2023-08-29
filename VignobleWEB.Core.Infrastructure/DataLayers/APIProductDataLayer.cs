using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Token;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIProductDataLayer : IProductDataLayer
    {
        #region Champs
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private readonly ITokenAPI _tokenAPI;

        //private string baseUrl = config["ConnectionStrings:UrlAPIConnection"];
        #endregion

        #region Constructeur
        public APIProductDataLayer(ITokenAPI tokenAPI) 
        {
            _tokenAPI = tokenAPI;
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public List<Product> GetAllProducts()
        {
            string url = $"{config["ConnectionStrings:UrlAPIConnection"]}/Product/GetAllProducts";
            string token = _tokenAPI.readTokenAPI();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _tokenAPI.getTokenAPI();
                    token = _tokenAPI.readTokenAPI();
                    client.DefaultRequestHeaders.Remove("Authorization");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                }

                response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode) { throw new DataLayersException(response.StatusCode.ToString()); }

                string json = response.Content.ReadAsStringAsync().Result;

                List<Product> listProducts = JsonConvert.DeserializeObject<List<Product>>(json);

                return listProducts;
            }
        }
        #endregion

        #endregion

        #region Méthodes privées
        #endregion
    }
}
