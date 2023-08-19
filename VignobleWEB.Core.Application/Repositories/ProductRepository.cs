using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public ProductRepository(IProductDataLayer dataLayer, ILogRepository logRepository)
        {
            _dataLayer = dataLayer;
            _logRepository = logRepository;
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public List<Product> GetAllProducts()
        {
            try
            {
                return _dataLayer.GetAllProducts();
            }
            catch(DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #endregion
    }
}
