using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages
{
    public class BasketModel : PageModel
    {

        private readonly ILogRepository _logTools;
        private readonly IProductRepository _productRepository;

        public BasketModel(ILogRepository logTools, IProductRepository productRepository)
        {
            _logTools = logTools;
            _productRepository = productRepository;
        }

        #region M�thodes publique

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            try
            {
                Exemple();
            }
            catch (Exception ex)
            {
                _logTools.LogErreur("Une erreur s'est produite lors du GET sur la page panier !", ex);
            }

            return result;
        }

        public IActionResult OnPost()
        {
            IActionResult result = Page();

            try
            {

            }
            catch (Exception ex)
            {
                _logTools.LogErreur("Une erreur s'est produite lors du POST sur la page panier !", ex);
            }

            return result;
        }
        #endregion

        #region Méthodes privées
        private void Exemple()
        {
            //listProducts = _productRepository.GetAllActiveProducts().Result;

            listProducts.Add(new Product
            {
                Description = "Descripton produit",
                Name = "Nom produit",
                Image = "/img/img1.jpg",
                UnitPrice = 12
            });
            nbProduits = listProducts.Count;
        }
        #endregion

        #region Propriétés
        public List<Product> listProducts = new List<Product>();
        public int nbProduits = 0;
        #endregion
    }
}