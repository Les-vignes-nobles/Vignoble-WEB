using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                GetListProducts(string.Empty, 0);
                InitFilter();
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
                GetListProducts(SearchProduct, ChoiceFilter);
                InitFilter();
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
        private void GetListProducts(string searchProduct, int choiceFilter = 0)
        {
            ListProducts = _productRepository.GetAllActiveProductsResearch(searchProduct, ChoiceFilter);
            SearchProduct = searchProduct;
            ChoiceFilter = choiceFilter;
        }

        private void InitFilter()
        {
            FilterList.Add(new SelectListItem
            {
                Text = "",
                Value = Convert.ToString(0)
            });

            FilterList.Add(new SelectListItem
            {
                Text = "Par ordre alphabétique",
                Value = Convert.ToString(1)
            });

            FilterList.Add(new SelectListItem
            {
                Text = "Par prix croissant",
                Value = Convert.ToString(2)
            });

            FilterList.Add(new SelectListItem
            {
                Text = "Par prix décroissant",
                Value = Convert.ToString(3)
            });

            FilterList.Add(new SelectListItem
            {
                Text = "Par popularité",
                Value = Convert.ToString(4)
            });
        }
        #endregion

        #region Propriétés
        [BindProperty] public string SearchProduct { get; set; } = string.Empty;
        [BindProperty] public int ChoiceFilter { get; set; } = 0;
        public List<SelectListItem> FilterList { get; set; } = new List<SelectListItem>();
        public List<Product> ListProducts { get; set; } = new List<Product>();
        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };

        #endregion
    }
}