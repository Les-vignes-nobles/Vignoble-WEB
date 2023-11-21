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
    public class PictureRepository : IPictureRepository
    {
        #region Champs
        private readonly IPictureDataLayer _pictureDataLayer;
        #endregion

        #region Constructeur
        public PictureRepository(IPictureDataLayer pictureDataLayer)
        {
            _pictureDataLayer = pictureDataLayer;
        }
        #endregion

        #region Méthodes publiques

        #region Read
        public async Task<Picture> GetImageById(string pictureId)
        {
            try
            {
                return _pictureDataLayer.GetImageById(pictureId).Result;
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
