using Microsoft.Extensions.Options;
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

        private readonly IOptions<ApiSettings> _config;
        private readonly ITokenAPI _tokenAPI;
        #endregion

        #region Constructeur
        public APIProductDataLayer(IOptions<ApiSettings> config, ITokenAPI tokenAPI)
        {
            _config = config;
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public async Task<List<Product>> GetAllProducts()
        {
            string token = _tokenAPI.ReadTokenAPI();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.Value.BaseUrl ?? "");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/Product").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    token = await _tokenAPI.GetTokenAPI();
                    client.DefaultRequestHeaders.Remove("Authorization");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    response = await client.GetAsync($"{client.BaseAddress}/Product/GetAllProducts");

                    if (!response.IsSuccessStatusCode)
                        throw new DataLayersException(response.StatusCode.ToString());

                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();

                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
                }
            }
        }
        #endregion

        #endregion

        #region Méthodes privées
        #endregion
    }
}
