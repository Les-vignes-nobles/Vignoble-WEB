using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Models;
using VignobleWEB.Core.Models.Interne;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface IHeaderOrderDataLayer
    {
        #region Create 
        Task<bool> CreateOrder(CreateOrderDto createOrderDto);
        #endregion

        #region Read
        /// <summary>
        /// Permet de récuperer la lsite des entete de commande du client
        /// </summary>
        /// <param name="idUser">Id de l'user</param>
        /// <returns>Liste des entetes</returns>
        Task<List<HeaderOrder>> GetListHeaderOrderByCustomer(Guid idUser);

        /// <summary>
        /// Permet de récuperer un entete de commande
        /// </summary>
        /// <param name="idHeaderOrder">Id de l'entete de commande</param>
        /// <returns></returns>
        Task<HeaderOrder> GetHeaderOrderById(Guid idHeaderOrder);
        #endregion

        #region Update
        #endregion

        #region delete
        #endregion
    }
}
