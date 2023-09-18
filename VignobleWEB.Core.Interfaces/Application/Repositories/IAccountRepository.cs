using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Application.Repositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Envoie la requête à l'API pour créer le compte
        /// </summary>
        /// <returns></returns>
        Task<User> CreateUser();
    }
}
