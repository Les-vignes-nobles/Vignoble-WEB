using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Application.Tools;
using VignobleWEB.Core.Infrastructure.Token;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Basket;

public class BasketModel : PageModel
{
    #region Champs
    private readonly ILogRepository _logRepository;
    private readonly IProductRepository _productRepository;
    private readonly ITransportRepository _transportRepository;
    #endregion

    #region Constructeur
    public BasketModel(ILogRepository logRepository, IProductRepository productRepository,
        ITransportRepository transportRepository)
    {
        _logRepository = logRepository;
        _productRepository = productRepository;
        _transportRepository = transportRepository;
    }
    #endregion

    #region M�thodes publique

    public async Task<IActionResult> OnGet()
    {
        IActionResult result = Page();

        try
        {
            listProducts = await _productRepository.GetAllActiveProducts();
            listTransports = await _transportRepository.GetAllActiveTransports();

            listSelectedTransports = new SelectList(
                listTransports.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                "Value", "Text");

            RecupListePanier();
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
    private void RecupListePanier()
    {
        listProducts = _productRepository.GetAllActiveProducts().Result;

        string panier = HttpContext.Session.GetString("panier");

        if (panier == null)
            return;
        string[] listIds = panier.Split("/");

        foreach (string id in listIds)
        {
            foreach (var product in listProducts)
            {
                if (product.Id.ToString() == id)
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

    public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
    public int Id { get; set; } = 1;
    public List<Product> listProducts;
    public List<Transport> listTransports;
    public SelectList listSelectedTransports;

    public string Token { get; } = TokenManager.Instance.GetToken();
    public int nbProduits;
    public double prixTotal;

    #endregion
}