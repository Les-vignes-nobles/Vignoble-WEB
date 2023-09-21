using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Application.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        #region Champs
        private readonly IAccountDataLayer _accountDataLayer;
        #endregion

        #region Constructeur
        public AccountRepository(IAccountDataLayer accountDataLayer)
        {
            _accountDataLayer = accountDataLayer; 
        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public bool CreateUser(User user, Customer customer)
        {
            try
            {
                VerifDonneesCreationUser(user, customer);

                _accountDataLayer.CreateUser(user, customer).Result;

                return true;
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException("Une erreur s'est produite avec l'API : " + ex.Message);
            }

        }
        #endregion

        #region Read (Lecture)
        #endregion

        #region Update (Modification)
        #endregion

        #region Delete (Suppression)
        #endregion

        #endregion

        #region Méthodes privées
        private void VerifDonneesCreationUser(User user, Customer customer)
        {
            if (user.Email == null || user.Email == string.Empty) { throw new RepositoryException("L'adresse mail ne peut pas être vide !"); }
            if (user.BirthDay == null || user.BirthDay >= DateTime.Today) { throw new RepositoryException("La date n'est pas valide !"); }
            if (user.EncryptPassword == null) { throw new RepositoryException("Le mot de passe ne peut pas être nul !"); }

            if (customer.Country == null || customer.Country == string.Empty) { throw new RepositoryException("Le pays ne peut pas être vide !"); }
            if (customer.Town == null || customer.Town == string.Empty) { throw new RepositoryException("La ville ne peut pas être nul !"); }
            if (customer.Address == null || customer.Address == string.Empty) { throw new RepositoryException("L'adresse ne doit pas être vide !"); }
            if (customer.CustomerName == null || customer.CustomerName == string.Empty) { throw new RepositoryException("Le prénom ne peut pas être vide !"); }
            if (customer.CustomerSurname == null || customer.CustomerSurname == string.Empty) { throw new RepositoryException("Le nom ne peut pas être vide !"); }
            if (customer.Email == null || customer.Email == string.Empty) { throw new RepositoryException("L'adresse mail ne peut pas être vide !"); }
            if (customer.PhoneNumber == null || customer.PhoneNumber.ToString().Length == 0) { throw new RepositoryException("Le nuémro de téléphone ne peut pas être vide !"); }
        }
        #endregion
    }
}
