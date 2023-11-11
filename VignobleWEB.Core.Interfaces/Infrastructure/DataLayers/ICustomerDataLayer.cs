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
        /// <summary>
        /// Recupere l'adresse de l'utilisateur
        /// </summary>
        /// <param name="idUser">id de l'utilisateur</param>
        /// <returns>L'adresse de l'utilisateur</returns>
        Task<Customer> GetAdress(int idUser);
    }
}
