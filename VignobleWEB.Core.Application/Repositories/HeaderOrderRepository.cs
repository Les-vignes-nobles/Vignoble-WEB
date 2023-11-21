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
    public class HeaderOrderRepository : IHeaderOrderRepository
    {
        #region Champs
        private readonly IHeaderOrderDataLayer _dataLayer;
        #endregion

        #region Constructeur
        public HeaderOrderRepository(IHeaderOrderDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public Task<List<HeaderOrder>> RecupererListeEnteteCommandeDunClient(Guid idUser)
        {
            try
            {
                return _dataLayer.RecupererListeEnteteCommandeDunClient(idUser);
            }
            catch (DataLayersException ex)
            {

                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #endregion
    }
}
