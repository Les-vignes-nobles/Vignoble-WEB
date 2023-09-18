using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Infrastructure.DataLayers
{
    public class APIAccountDataLayer : IAccountDataLayer
    {
        #region Champs

        #endregion

        #region Constructeur
        public APIAccountDataLayer() 
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
