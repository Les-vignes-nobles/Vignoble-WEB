using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Application.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        #region Champs
        private readonly IExampleDataLayer _dataLayer;
        private readonly ILogRepository _logInfrastructure;
        #endregion

        #region Constructeur
        public ExampleRepository(IExampleDataLayer dataLayer, ILogRepository logInfrastructure)
        {
            _dataLayer = dataLayer;
            _logInfrastructure = logInfrastructure;
        }
        #endregion

        public Example RecupererUnExample(int idExample)
        {
            try
            {
                return _dataLayer.RecupererUnExample(idExample);
            }
            catch(DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la base de données : {ex.Message}");
            }
        }


    }
}
