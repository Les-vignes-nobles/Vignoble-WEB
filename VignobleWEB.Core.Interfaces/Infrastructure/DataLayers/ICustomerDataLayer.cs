using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface ICustomerDataLayer
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
        /// <param name="mailuser">mail de l'utilisateur</param>
        /// <returns>L'adresse de l'utilisateur</returns>
        Task<Customer> GetAddress(string mailuser);
        #endregion

        #region Update
        /// <summary>
        /// Permet de modifier l'addresse de livraison
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<bool> UpdateAddress(Customer customer);
        #endregion

        #region Delete
        #endregion
    }
}
