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

        #region Create (Ajout)
        public async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                VerifIntegriteDonnees(customer);

                return await _dataLayer.CreateCustomer(customer);
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #region Read (Lecture)
        public async Task<Customer> GetAddress(string guidUser)
        {
            try
            {
                return _dataLayer.GetAddress(guidUser).Result;
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #region Update (Modification)
        public async Task<bool> UpdateAddress(Customer customer)
        {
            try
            {
                VerifIntegriteDonnees(customer);

                return await _dataLayer.CreateCustomer(customer);
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #endregion

        #region Méthodes privées
        private void VerifIntegriteDonnees(Customer customer)
        {
            if (customer == null) { throw new RepositoryException("L'adresse de livraison ne peut pas être vide !"); }
            if (customer.User == null) { throw new RepositoryException("L'utilsateur ne peut pas être vide !"); }

            if (customer.CustomerName == null || customer.CustomerName == string.Empty) { throw new RepositoryException("Le prénom ne peut pas être vide !"); }
            if (customer.CustomerSurname == null || customer.CustomerSurname == string.Empty) { throw new RepositoryException("Le nom ne peut pas être vide !"); }
            if (customer.Address == null || customer.Address == string.Empty) { throw new RepositoryException("L'adresse ne peut pas être vide !"); }
            if (customer.Town == null || customer.Town == string.Empty) { throw new RepositoryException("La ville ne peut pas être vide !"); }
            if (customer.ZipCode == null || customer.ZipCode == 0) { throw new RepositoryException("Le code postal ne peut pas être vide !"); }
            if (customer.Country == null || customer.Country == string.Empty) { throw new RepositoryException("Le pays ne peut pas être vide !"); }
            if (customer.PhoneNumber == null || customer.PhoneNumber == string.Empty) { throw new RepositoryException("Le numéro de téléphone ne peut pas être vide !"); }
            if (customer.Email == null || customer.Email == string.Empty) { throw new RepositoryException("L'adresse mail ne peut pas être vide !"); }
        }
        #endregion
    }
}
