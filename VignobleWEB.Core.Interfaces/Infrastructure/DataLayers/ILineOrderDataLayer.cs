using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface ILineOrderDataLayer
    {
        /// <summary>
        /// Permet de récuperer les lignes de commande d'une commande
        /// </summary>
        /// <param name="headerOrder"></param>
        /// <returns></returns>
        Task<List<LineOrder>> GetLinesOrderByHeaderOrder(Guid headerOrder);
    }
}
