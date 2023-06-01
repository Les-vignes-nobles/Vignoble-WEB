using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface IExampleDataLayer
    {
        #region Read (Lecture)
        /// <summary>
        /// Permet de récupérer un exemple
        /// </summary>
        /// <param name="idExample">id</param>
        /// <returns>Liste d'example</returns>
        Example RecupererUnExample(int idExample);
        #endregion
    }
}
