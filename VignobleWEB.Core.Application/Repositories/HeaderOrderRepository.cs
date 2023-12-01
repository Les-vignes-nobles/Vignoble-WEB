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
        public bool CreateOrder(HeaderOrder headerOrder, List<LineOrder> lineOrders)
        {
            try
            {
                CheckDataOrder(headerOrder, lineOrders);

                _dataLayer.CreateOrder(headerOrder, lineOrders);
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
            return true;
        }
        #endregion

        #region Read (Lecture)
        public Task<List<HeaderOrder>> RecupererListeEnteteCommandeDunClient(Guid idUser)
        {
            try
            {
                return _dataLayer.RecupererListeEnteteCommandeDunClient(idUser);
            }
            catch (DataLayersException ex)
            {
                throw new RepositoryException($"Une erreur s'est produite dans la récupération des données via l'API : {ex.Message}");
            }
        }
        #endregion

        #endregion

        #region Méthodes privées
        private void CheckDataOrder(HeaderOrder headerOrder, List<LineOrder> lineOrders)
        {
            if (headerOrder.NumOrder == 0|| headerOrder.NumOrder == null) { throw new RepositoryException("Le numéro de commande ne peut pas être nul !"); }
            if (headerOrder.Customer == null) { throw new RepositoryException("L'adresse de livraison ne peut pas être vide !"); }
            if (headerOrder.CustomerId == null) { throw new RepositoryException("L'id de l'adresse de livraison ne peut pas être nul"); }
            if (headerOrder.SupplierId == null) { throw new RepositoryException("L'id du fournisseur ne peut pas être nul"); }
            if (headerOrder.Supplier == null) { throw new RepositoryException("Le fournisseur ne peut pas être nul"); }
            if (headerOrder.Date == null) { throw new RepositoryException("La date de création de la commande ne peut pas être nulle !"); }
        }
        #endregion
    }
}
