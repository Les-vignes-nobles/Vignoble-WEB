using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                RecupListePanier();
                RecupListeTransport();
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
        private void RecupListeTransport()
        {
            List<Carrier> listCarriers = new();

            listCarriers.Add(new Carrier
            {
                Id = 1,
                Name = "Colissimo",
                Price = 6
            });
            listCarriers.Add(new Carrier
            {
                Id = 2,
                Name = "Chronopost",
                Price = 12
            });

            listTransports.Clear();
            listTransports.Add(new SelectListItem
            {
                Value = Convert.ToString(0),
                Text = "Veuillez choisir le transporteur",
                Selected = true
            });

            foreach (var item in listCarriers)
            {
                listTransports.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name + " - " + item.Price + " €"
                });
            }
        }

        private void RecupListePanier()
        {
            //listProducts = _productRepository.GetAllActiveProducts().Result;
            List<Product> listProductsTest = new();


            listProductsTest.Add(new Product
            {
                Id = 0,
                Description = "Descripton produit",
                Name = "Nom produit",
                Image = "/img/img1.jpg",
                UnitPrice = 12
            });
            listProductsTest.Add(new Product
            {
                Id = 1,
                Description = "Descripton produit1",
                Name = "Nom produit1",
                Image = "/img/img4.jpg",
                UnitPrice = 12.5
            });
            listProductsTest.Add(new Product
            {
                Id = 2,
                Description = "Descripton produit2",
                Name = "Nom produit2",
                Image = "/img/img1.jpg",
                UnitPrice = 12
            });
            listProductsTest.Add(new Product
            {
                Id = 3,
                Description = "Descripton produit3",
                Name = "Nom produit3",
                Image = "/img/img5.jpg",
                UnitPrice = 12
            });
            listProductsTest.Add(new Product
            {
                Id = 4,
                Description = "Descripton produit4",
                Name = "Nom produit4",
                Image = "/img/img4.jpg",
                UnitPrice = 12
            });
            listProductsTest.Add(new Product
            {
                Id = 5,
                Description = "Descripton produit5",
                Name = "Nom produit5",
                Image = "/img/img5.jpg",
                UnitPrice = 12
            });

            string panier = HttpContext.Session.GetString("panier");

            string[] listIds = panier.Split("/");

            foreach(string id in listIds)
            {
                foreach(Product product in listProductsTest)
                {
                    if (product.Id == Convert.ToInt32(id))
                    {
                        listProducts.Add(product);
                        prixTotal += product.UnitPrice;
                    }
                }
            }
            nbProduits = listProducts.Count;
        }
        #endregion

        #region Propriétés
        public List<Product> listProducts = new List<Product>();
        public List<SelectListItem> listTransports = new();
        [BindProperty]
        public Carrier carrier { get; set; } = new Carrier();
        public int nbProduits = 0;
        public double prixTotal = 0;
        
        #endregion
    }
}