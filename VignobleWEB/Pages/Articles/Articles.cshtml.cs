using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages
{
    public class ArticlesModel : PageModel
    {
        #region Champs
        private readonly ILogRepository _logTools;
        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructeur
        public ArticlesModel(ILogRepository logTools, IProductRepository productRepository)
        {
            _logTools = logTools;
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
            catch (Exception ex)
            {
                _logTools.LogErreur("Une erreur s'est produite lors du GET sur la page index des r�f�rences (Liste) !", ex);
            }

            return result;
        }

        #endregion

        #region M�thodes priv�es
        private void RecupererListeProduits()
        {
            ListProducts = _productRepository.GetAllProducts();
        }
        #endregion

        #region Propriétés
        public List<Product> ListProducts { get; set; } = new List<Product>();

        #endregion
    }
}