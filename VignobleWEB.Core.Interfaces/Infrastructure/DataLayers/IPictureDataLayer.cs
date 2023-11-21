using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface IPictureDataLayer
    {
        /// <summary>
        /// Récupere un les données de l'image de larticle
        /// </summary>
        /// <returns>Retourne les données de l'image</returns>
        Task<Picture> GetImageById(string pictureId);
    }
}
