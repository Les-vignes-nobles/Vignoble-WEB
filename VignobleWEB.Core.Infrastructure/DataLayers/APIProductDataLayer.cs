using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIProductDataLayer : IProductDataLayer
    {
        #region Champs
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private const string baseUrl = config["ConnectionStrings:UrlAPIConnection"];
        #endregion

        #region Constructeur
        public APIProductDataLayer() 
        {
        
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public List<Product> GetAllProducts()
        {
            string url = $"{baseUrl}/GetAllProducts";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIzZDVmYzVjZC1mZDJkLTQ5NGEtOGEyZi1iYWU2NTgxMTYwNzciLCJpYXQiOiIwOC8yMS8yMDIzIDEwOjUwOjIzIiwiVXNlcklkIjoiMSIsIkRpc3BsYXlOYW1lIjoiVXNlcjEiLCJleHAiOjE2OTI2MTg2MjMsImlzcyI6IkpXVFZpZ25vYmxlQVBJIiwiYXVkIjoiSldUU2VydmljZVZpZ25vYmxlQVBJIn0.WOn0UP-mGO-z3SYya_wjE0X8nA5anbXiO8hT9Nitj3g");

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode) { throw new DataLayersException(response.StatusCode.ToString()); }

                string json = response.Content.ReadAsStringAsync().Result;

                List<Product> listProducts = JsonConvert.DeserializeObject<List<Product>>(json);

                return listProducts;
            }
        }
        #endregion

        #endregion

        #region Méthodes publiques
        #endregion
    }
}
