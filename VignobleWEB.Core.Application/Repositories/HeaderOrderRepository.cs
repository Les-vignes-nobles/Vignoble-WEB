using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Infrastructure.ExceptionPersonnalisee;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.DataLayers;
using VignobleWEB.Core.Models;
using VignobleWEB.Core.Models.Interne;

namespace VignobleWEB.Core.Application.Repositories
{
    public class HeaderOrderRepository : IHeaderOrderRepository
    {
        #region Champs
        private readonly IHeaderOrderDataLayer _dataLayer;
        #endregion

        #region Constructeur
        public HeaderOrderRepository(IHeaderOrderDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        #endregion

        #region Méthodes publiques

        #region Create (Ajout)
        public async Task<bool> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                CheckDataOrder(createOrderDto);

                CreateOrderDto createOrder = new CreateOrderDto
                {
                    Status = createOrderDto.Status,
                    Date = createOrderDto.Date,
                    Paid = createOrderDto.Paid,
                    SupplierId = createOrderDto.SupplierId,
                    CustomerId = createOrderDto.CustomerId,
                    LineOrders = createOrderDto.LineOrders
                };

                return await _dataLayer.CreateOrder(createOrder);
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #region Read (Lecture)
        public Task<List<HeaderOrder>> GetListHeaderOrderByCustomer(Guid idUser)
        {
            try
            {
                return _dataLayer.GetListHeaderOrderByCustomer(idUser);
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }

        public async Task<HeaderOrder> GetHeaderOrderById(Guid idHeaderOrder)
        {
            try
            {
                return await _dataLayer.GetHeaderOrderById(idHeaderOrder);
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #endregion

        #region Méthodes privées
        private void CheckDataOrder(CreateOrderDto createOrderDto)
        {
            if (createOrderDto.Status == null) { throw new RepositoryException("Le status ne peut pas être nul !"); }
            if (createOrderDto.Date == null) { throw new RepositoryException("La date de création de la commande ne peut pas être nulle !"); }
            if (createOrderDto.CustomerId == null) { throw new RepositoryException("L'adresse de livraison ne peut pas être vide"); }
            if (createOrderDto.LineOrders == null) { throw new RepositoryException("La commande doit contenir au moins une ligne de commande !"); }

            foreach (LineOrderDto line in createOrderDto.LineOrders)
            {
                if (line.HeaderOrderId == null) { throw new RepositoryException("L'entête de commande ne peut pas être nul !"); }
                if (line.Quantity == null || line.Quantity == 0) { throw new RepositoryException("La quantité ne peut pas être égale à 0 !"); }
                if (line.ProductId == null) { throw new RepositoryException("L'article ne peut pas être nul !"); }
            }
        }
        #endregion
    }
}
