using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Application.Repositories
{
    public interface ICustomerRepository
    {
        #region Create
        /// <summary>
        /// Permet de créer une adresse de livraison pour le client
        /// </summary>
        /// <param name="customer">Objet adresse</param>
        /// <returns>True = Réussite, </returns>
        Task<bool> CreateCustomer(Customer customer);
        #endregion

        #region Read 
        /// <summary>
        /// Recupere l'adresse de l'utilisateur
        /// </summary>
        /// <param name="guidUser">id de l'utilisateur</param>
        /// <returns>L'adresse de l'utilisateur</returns>
        Task<Customer> GetAddress(string guidUser);
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}
