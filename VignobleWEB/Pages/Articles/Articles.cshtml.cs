using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Drawing;
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
                RecupererListeProduits(string.Empty);
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

        public IActionResult OnPost()
        {
            IActionResult result = Page();

            try
            {
                RecupererListeProduits(SearchProduct);
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
        private void RecupererListeProduits(string searchProduct)
        {
            ListProducts = _productRepository.GetAllActiveProductsResearch(searchProduct);
            SearchProduct = searchProduct;
        }
        #endregion

        #region Propriétés
        [BindProperty] public string SearchProduct { get; set; } = string.Empty;
        public List<Product> ListProducts { get; set; } = new List<Product>();
        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };

        #endregion
    }
}