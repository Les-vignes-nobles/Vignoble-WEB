using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        #region Champs
        private readonly IProductRepository _productRepository;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public DetailsModel(IProductRepository productRepository, ILogRepository logRepository)
        {
            _productRepository = productRepository;
            _logRepository = logRepository;
        }
        #endregion

        #region M�thodes publiques
        public IActionResult OnGet()
        {
            IActionResult result = Page();

            try
            {
                string idReference = HttpContext.Request.RouteValues["idReference"].ToString();
                getProduct(idReference);
                getAllProducts();
            }
            catch (RepositoryException ex)
            {
                _logRepository.LogAvertissement(ex.Message);
            }
            catch (Exception ex)
            {
                MessagePourLaModal.Message = "Une erreur impr�vue s'est produite, si le probl�me perciste contacter le service informatique";
                _logRepository.LogErreur("Une erreut impr�vu s'est produite !", ex);
            }

            return result;
        }
        #endregion

        #region M�thodes priv�es
        private void getProduct(string guidProduct)
        {
            ProductDetails = _productRepository.GetProductById(guidProduct).Result;
        }
        private void getAllProducts()
        {
            Products = _productRepository.GetAllActiveProducts().Result;
        }
        #endregion

        #region Propri�t�s
        public Product ProductDetails { get; set; } = new Product();
        public List<Product> Products { get; set; } = new List<Product>();
        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
        #endregion
    }
}
