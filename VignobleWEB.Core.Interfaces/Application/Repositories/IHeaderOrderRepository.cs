using VignobleWEB.Core.Models;
using VignobleWEB.Core.Models.Interne;

namespace VignobleWEB.Core.Interfaces.Application.Repositories
{
    public interface IHeaderOrderRepository
    {
        #region Create 
        Task<bool> CreateOrder(CreateOrderDto createOrderDto);
        #endregion

        #region Read
        /// <summary>
        /// Permet de récuperer la liste des entetes de commandes du client
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
