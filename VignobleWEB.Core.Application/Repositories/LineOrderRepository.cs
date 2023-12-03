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
    public class LineOrderRepository : ILineOrderRepository
    {
        #region Champs
        private readonly ILineOrderDataLayer _dataLayer;
        #endregion

        #region Constructeur
        public LineOrderRepository(ILineOrderDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        #endregion

        #region Méthodes publiques

        #region Read (Lecture)
        public Task<List<LineOrder>> GetLinesOrderByHeaderOrder(Guid headerOrder)
        {
            try
            {
                return _dataLayer.GetLinesOrderByHeaderOrder(headerOrder);
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
