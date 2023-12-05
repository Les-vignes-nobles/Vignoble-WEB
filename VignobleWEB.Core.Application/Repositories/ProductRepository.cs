using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Application.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Champs
        private readonly IProductDataLayer _dataLayer;
        private readonly IPictureRepository _pictureRepository;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public ProductRepository(IProductDataLayer dataLayer, ILogRepository logRepository, IPictureRepository pictureRepository)
        {
            _dataLayer = dataLayer;
            _logRepository = logRepository;
            _pictureRepository = pictureRepository;
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public async Task<List<Product>> GetAllActiveProducts()
        {
            try
            {
                List<Product> listAllProducts = await _dataLayer.GetAllProducts();
                
                List<Product> listActiveProducts = new List<Product>();

                foreach (Product product in listAllProducts)
                {
                    listActiveProducts.Add(product);
                }
                
                return listActiveProducts;
            }
            catch(DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }

        public async Task<Product> GetProductById(string productId)
        {
            try
            {
                Product product = _dataLayer.GetProductById(productId).Result;

                Picture picture = _pictureRepository.GetImageById(product.PictureId).Result;
                product.Picture = picture;

                return product;

            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }

        public List<Product> GetAllActiveProductsResearch(string searchProduct, int choiceFilter = 0)
        {
            List<Product> listActiveProduct = GetAllActiveProducts().Result;

            if (searchProduct.IsNullOrEmpty() && choiceFilter > 0)
            {
                List<Product> listProductFilter = new List<Product>();

                if (choiceFilter == 1)
                {
                    foreach (Product product in listActiveProduct.OrderBy(item => item.Name))
                    {
                        listProductFilter.Add(product);
                    }
                }

                if (choiceFilter == 2)
                {
                    foreach (Product product in listActiveProduct.OrderBy(item => item.UnitPrice))
                    {
                        listProductFilter.Add(product);
                    }
                }

                if (choiceFilter == 3)
                {
                    foreach (Product product in listActiveProduct.OrderByDescending(item => item.UnitPrice))
                    {
                        listProductFilter.Add(product);
                    }
                }

                if (choiceFilter == 4)
                {
                    foreach (Product product in listActiveProduct.OrderBy(item => item.Year))//TODO : Avoir l'article le plus populaire en 1er
                    {
                        listProductFilter.Add(product);
                    }
                }

                return listProductFilter;
            }
            else if (searchProduct != string.Empty && choiceFilter == 0)
            {
                List<Product> listActiveProductResearch= new List<Product>();

                foreach (Product product in listActiveProduct)
                {
                    if(product.Name.ToLower().Contains(searchProduct.ToLower()) || product.Description.ToLower().Contains(searchProduct.ToLower()))
                    {
                        listActiveProductResearch.Add(product);
                    }
                }

                return listActiveProductResearch;
            }
            else if (searchProduct != string.Empty && choiceFilter > 0)
            {
                List<Product> listActiveProductResearch = new List<Product>();

                foreach (Product product in listActiveProduct)
                {
                    if (product.Name.ToLower().Contains(searchProduct.ToLower()) || product.Description.ToLower().Contains(searchProduct.ToLower()))
                    {
                        listActiveProductResearch.Add(product);
                    }
                }

                if (choiceFilter == 1)
                {
                    listActiveProductResearch.Sort();
                }

                if (choiceFilter == 2)
                {
                    listActiveProductResearch.OrderBy(x => x.UnitPrice);
                }

                if (choiceFilter == 3)
                {
                    listActiveProductResearch.OrderByDescending(x => x.UnitPrice);
                }

                if (choiceFilter == 4)
                {
                    listActiveProductResearch.OrderBy(item => item.Year); //TODO : Avoir l'article le plus populaire en 1er
                }

                return listActiveProductResearch;
            }
            else
            {
                return listActiveProduct;
            }
        }
        #endregion

        #endregion
    }
}
