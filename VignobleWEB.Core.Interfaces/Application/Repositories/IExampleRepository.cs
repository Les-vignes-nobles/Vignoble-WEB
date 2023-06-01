using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Application.Repositories
{
    public interface IExampleRepository
    {
        #region Read (Lecture)
        /// <summary>
        /// Permet de récuperer un exemple
        /// </summary>
        /// <param name="idExample">id</param>
        /// <returns>exemple</returns>
        Example RecupererUnExample(int idExample);
        #endregion
    }
}
