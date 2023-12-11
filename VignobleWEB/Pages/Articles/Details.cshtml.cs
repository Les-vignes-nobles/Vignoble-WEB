using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;
using VignobleWEB.Core.Models.Interne;

namespace VignobleWEB.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        #region Champs
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public DetailsModel(IProductRepository productRepository, ILogRepository logRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _logRepository = logRepository;
            _stockRepository = stockRepository;
        }
        #endregion

        #region Méthodes publiques
        public IActionResult OnGet()
        {
            IActionResult result = Page();

            try
            {
                string idReference = HttpContext.Request.RouteValues["idReference"].ToString();
                getProduct(idReference);
                getAllProducts(idReference);
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
                string idReference = HttpContext.Request.RouteValues["idReference"].ToString();
                getProduct(idReference);
                getAllProducts(idReference);

                if (Request.Cookies["CardItem"] == null)
                {
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(1);
                    cookieOptions.Path = "/";

                    listCardItems.Add(new CardItem
                    {
                        IdProduct = CardItem.IdProduct,
                        Quantity = CardItem.Quantity
                    });

                    Response.Cookies.Append("CardItem", JsonConvert.SerializeObject(listCardItems), cookieOptions);
                }
                else
                {
                    listCardItems = JsonConvert.DeserializeObject<List<CardItem>>(Request.Cookies["CardItem"]);

                    if (listCardItems != null)
                    {
                        if (listCardItems.Where(item => item.IdProduct == CardItem.IdProduct).Count() > 0)
                        {
                            foreach (CardItem item in listCardItems)
                            {
                                if (item.IdProduct == CardItem.IdProduct)
                                {
                                    item.Quantity += CardItem.Quantity;
                                }
                            }
                        }
                        else
                        {
                            listCardItems.Add(new CardItem
                            {
                                IdProduct = CardItem.IdProduct,
                                Quantity = CardItem.Quantity
                            });
                        }
                    }
                    else
                    {
                        listCardItems = new List<CardItem>();

                        listCardItems.Add(new CardItem
                        {
                            IdProduct = CardItem.IdProduct,
                            Quantity = CardItem.Quantity
                        });
                    }

                    Response.Cookies.Append("CardItem", JsonConvert.SerializeObject(listCardItems));
                }
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

        #region Méthodes privées
        private void getProduct(string guidProduct)
        {
            ProductDetails = _productRepository.GetProductById(guidProduct).Result;
            ProductDetails.Stock = _stockRepository.GetStockById(ProductDetails.StockId).Result; 

        }
        private void getAllProducts(string idReference)
        {
            List<Product> allProducts = _productRepository.GetAllActiveProducts().Result;

            foreach (Product product in allProducts)
            {
                if (product.Id.ToString() != idReference)
                {
                    Products.Add(product);
                }
            }
        }
        #endregion

        #region Propriétés
        public Product ProductDetails { get; set; } = new Product();
        public List<Product> Products { get; set; } = new List<Product>();
        [BindProperty] public CardItem CardItem { get; set; } = new();
        public List<CardItem> listCardItems { get; set; } = new();
        public MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
        #endregion
    }
}
