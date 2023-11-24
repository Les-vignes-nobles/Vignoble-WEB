using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Application.Tools;
using VignobleWEB.Core.Infrastructure.Token;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;
using VignobleWEB.Core.Models.Interne;

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
            GetListShop();
            GetListTransports();
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
    private void GetListTransports()
    {
        listTransports = _transportRepository.GetAllActiveTransports().Result;

        foreach (var item in listTransports)
        {
            listSelectedTransports.Add(new SelectListItem
            {
                Text = item.Name + " " + item.Price,
                Value = item.Id.ToString()
            }) ;
        }
    }

    private void GetListShop()
    {
        if(Request.Cookies["CardItem"] != null)
        {
            listCardItems = JsonConvert.DeserializeObject<List<CardItem>>(Request.Cookies["CardItem"]);

            if(listCardItems != null)
            {
                foreach (CardItem item in listCardItems)
                {
                    Product product = _productRepository.GetProductById(item.IdProduct).Result;
                    listProducts.Add(product);
                    nbProduitsTotal += item.Quantity;
                }
            }
        }

        nbProduits = listProducts.Count;
    }
    #endregion

    #region Propriétés

    public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
    public List<CardItem> listCardItems { get; set; } = new();
    public List<Product> listProducts { get; set; } = new();

    public List<SelectListItem> listSelectedTransports { get; set; } = new List<SelectListItem>();
    public Guid Id { get; set; }
    public List<Transport> listTransports { get; set; } = new List<Transport>();

    public string Token { get; } = TokenManager.Instance.GetToken();
    public int nbProduits;
    public double prixTotal;
    public int nbProduitsTotal;

    #endregion
}