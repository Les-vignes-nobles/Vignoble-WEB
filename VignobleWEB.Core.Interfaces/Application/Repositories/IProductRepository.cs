using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Application.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Récupere une liste de produits
        /// </summary>
        /// <returns>Retourne la liste de tout les produits</returns>
        Task<List<Product>> GetAllActiveProducts();

        /// <summary>
        /// Recupère les données du produits
        /// </summary>
        /// <param name="productId">Guid du produit</param>
        /// <returns>le produit</returns>
        Task<Product> GetProductById(string productId);
    }
}
