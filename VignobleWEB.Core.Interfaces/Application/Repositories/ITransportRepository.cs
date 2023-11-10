using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Application.Repositories;

public interface ITransportRepository
{
    Task<List<Transport>> GetAllActiveTransports();
}