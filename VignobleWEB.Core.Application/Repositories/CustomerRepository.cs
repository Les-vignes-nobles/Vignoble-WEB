using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Application.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        #region Champs 
        private readonly ICustomerDataLayer _dataLayer;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public CustomerRepository(ICustomerDataLayer dataLayer, ILogRepository logRepository) 
        {
            _dataLayer = dataLayer;
            _logRepository = logRepository;
        }
        #endregion

        #region Méthodes publiques
        public async Task<Customer> GetAdress(int idUser)
        {
            try
            {
                return _dataLayer.GetAdress(idUser).Result;
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #region Méthodes privées
        #endregion
    }
}
