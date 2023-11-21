using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Application.Tools;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Articles
{
    public class ArticlesModel : PageModel
    {
        #region Champs
        private readonly ILogRepository _logRepository;
        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructeur
        public ArticlesModel(ILogRepository logRepository, IProductRepository productRepository)
        {
            _logRepository = logRepository;
            _productRepository = productRepository;
        }
        #endregion

        #region M�thodes publique

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            try
            {
                RecupererListeProduits();
            }
            catch (RepositoryException ex)
            {
                _logRepository.LogAvertissement(ex.Message);
            }
            catch (Exception ex)
            {
                MessagePourLaModal.Message = "Une erreur imprévue s'est produite, si le problème perciste contacter le service informatique";
                _logRepository.LogErreur("Une erreut imprévu s'est produite !", ex);
            }

            return result;
        }

        #endregion

        #region M�thodes priv�es
        private void RecupererListeProduits()
        {
            ListProducts = _productRepository.GetAllActiveProducts().Result;
        }
        #endregion

        #region Propriétés
        public List<Product> ListProducts { get; set; } = new List<Product>();
        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };

        #endregion
    }
}