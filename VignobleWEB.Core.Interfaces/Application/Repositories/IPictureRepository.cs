using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Application.Repositories
{
    public interface IPictureRepository
    {
        /// <summary>
        /// Récupere un les données de l'image de larticle
        /// </summary>
        /// <returns>Retourne les données de l'image</returns>
        Task<Picture> GetImageById(string pictureId);
    }
}
