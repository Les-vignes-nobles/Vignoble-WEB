﻿using System.Reflection;
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
                    if (product.PictureId != null)
                    {
                        //product.Picture = _pictureRepository.GetImageById(product.PictureId).Result;
                    }
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
        #endregion

        #endregion
    }
}
