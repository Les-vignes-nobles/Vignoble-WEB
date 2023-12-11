using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface IStockDataLayer
    {
        Task<Stock> GetStockById(Guid id);
    }
}
