using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface IProductDataLayer
    {
        /// <summary>
        /// Récupere une liste de produits
        /// </summary>
        /// <returns>Retourne la liste de tout les produits</returns>
        Task<List<Product>> GetAllProducts();
    }
}
