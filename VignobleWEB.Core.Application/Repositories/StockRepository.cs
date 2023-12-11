using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Application.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IStockDataLayer _dataLayer;
        public StockRepository(IStockDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public Task<Stock> GetStockById(Guid id)
        {
            return _dataLayer.GetStockById(id);
        }
    }
}
