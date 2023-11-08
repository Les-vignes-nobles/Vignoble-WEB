using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;

public interface ITransportDataLayer
{
    Task<List<Transport>> GetAllTransports();

}