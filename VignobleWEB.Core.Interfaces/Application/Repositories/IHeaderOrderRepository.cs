using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Application.Repositories
{
    public interface IHeaderOrderRepository
    {
        #region Create 
        #endregion

        #region Read
        /// <summary>
        /// Permet de récuperer la lsite des entete de commande du client
        /// </summary>
        /// <param name="idUser">Id de l'user</param>
        /// <returns>Liste des entetes</returns>
        Task<List<HeaderOrder>> RecupererListeEnteteCommandeDunClient(Guid idUser);
        #endregion

        #region Update
        #endregion

        #region delete
        #endregion
    }
}
