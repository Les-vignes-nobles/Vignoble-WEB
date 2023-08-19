using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIProductDataLayer : IProductDataLayer
    {
        #region Champs
        private const string baseUrl = "http://82.165.237.163:5000/api/Product";
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
                client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiJhNTFlY2NlZi1iZmEyLTRkYWYtYjQyYy0wZTEwY2U3NWM2OWUiLCJpYXQiOiIwOC8xOS8yMDIzIDA5OjM3OjU0IiwiVXNlcklkIjoiMSIsIkRpc3BsYXlOYW1lIjoiVXNlcjEiLCJleHAiOjE2OTI0NDE0NzQsImlzcyI6IkpXVFZpZ25vYmxlQVBJIiwiYXVkIjoiSldUU2VydmljZVZpZ25vYmxlQVBJIn0.f5nF_DDodBqwOshLT8q0QnykMST4Hwj1kpzN-pmyJ_8");

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode) { throw new Exception(); }

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
