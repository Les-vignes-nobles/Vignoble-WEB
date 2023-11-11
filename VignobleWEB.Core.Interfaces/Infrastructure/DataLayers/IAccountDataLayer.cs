﻿using VignobleWEB.Core.Models;

namespace VignobleWEB.Core.Interfaces.Infrastructure.DataLayers
{
    public interface IAccountDataLayer
    {
        /// <summary>
        /// Envoie la requête à l'API pour créer le compte
        /// </summary>
        /// <returns></returns>
        Task<bool> CreateUser(User user, Customer customer);
    }
}
