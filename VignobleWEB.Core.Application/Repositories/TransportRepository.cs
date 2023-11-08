using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Application.Repositories;

public class TransportRepository : ITransportRepository
{

    #region Champs
    private readonly ITransportDataLayer _dataLayer;
    private readonly ILogRepository _logRepository;
    #endregion

    #region Constructeur
    public TransportRepository(ITransportDataLayer dataLayer, ILogRepository logRepository)
    {
        _dataLayer = dataLayer;
        _logRepository = logRepository;
    }
    #endregion

    #region Méthodes publiques

    #region Read (Lecture)
    public async Task<List<Transport>> GetAllActiveTransports()
    {
        try
        {
            List<Transport> listAllTransports = await _dataLayer.GetAllTransports();

            List<Transport> listActiveTransports = new List<Transport>();

            foreach (var product in listAllTransports)
            {
                listActiveTransports.Add(product);
            }

            return listActiveTransports;
        }
        catch(DataLayersException ex)
        {
            throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
        }
    }
    #endregion

    #endregion
}