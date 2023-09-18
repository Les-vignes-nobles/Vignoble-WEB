using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Application.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        #region Champs

        #endregion

        #region Constructeur
        public AccountRepository()
        {

        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public Task<User> CreateUser()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read (Lecture)
        #endregion

        #region Update (Modification)
        #endregion

        #region Delete (Suppression)
        #endregion

        #endregion
    }
}
