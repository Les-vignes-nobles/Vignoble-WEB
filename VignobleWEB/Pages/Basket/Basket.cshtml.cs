using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHeaderOrderRepository _headerOrderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAccountRepository _accountRepository;
    #endregion

    #region Constructeur
    public BasketModel(ILogRepository logRepository, IProductRepository productRepository,
        ITransportRepository transportRepository, UserManager<IdentityUser> userManager, IHeaderOrderRepository headerOrderRepository, 
        ICustomerRepository customerRepository, IAccountRepository accountRepository)
    {
        _logRepository = logRepository;
        _productRepository = productRepository;
        _transportRepository = transportRepository;
        _userManager = userManager;
        _headerOrderRepository = headerOrderRepository;
        _customerRepository = customerRepository;
        _accountRepository = accountRepository;
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

    public async Task<IActionResult> OnPost()
    {
        IActionResult result = Page();

        try
        {
            var res = Request.Cookies["CardItem"];
            var user = await _userManager.GetUserAsync(User);

            listCardItems = JsonConvert.DeserializeObject<List<CardItem>>(res);

            if (await _userManager.GetUserAsync(User) == null)
            {
                MessagePourLaModal.Message = "Vous devez être connecter pour valider la commande !";
            }
            else
            {
                if (listCardItems.Count != 0)
                {
                    if (transport.Id.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        MessagePourLaModal.Message = "Vous devez choisir un type de transport pour valider la commande !";
                    }
                    else
                    {
                        if(await CreateOrder(user))
                        {
                            var cookieOptions = new CookieOptions();
                            cookieOptions.Expires = DateTime.Now.AddDays(-30);
                            cookieOptions.Path = "/";

                            Response.Cookies.Append("CardItem", JsonConvert.SerializeObject(null), cookieOptions);

                            MessagePourLaModal.Titre = "Information";
                            MessagePourLaModal.Message = "La commande a bien été enregistré, vous pouvez la retouver dans votre historique de commande pour voir le suivi ! ";

                            GetListTransports();

                        }
                        else
                        {
                            MessagePourLaModal.Message = "Une erreur est surevenue lors de la création de la commande, veuillez contacter le support du site ";
                            GetListShop();
                            GetListTransports();
                        }
                    }
                }
                else
                {
                    MessagePourLaModal.Message = "Vous devez avoir au moins un article dans le panier !";
                    GetListShop();
                    GetListTransports();
                }
            }
        }
        catch (RepositoryException ex)
        {
            _logRepository.LogAvertissement(ex.Message);

            GetListShop();
            GetListTransports();
        }
        catch (Exception ex)
        {
            MessagePourLaModal.Message = "Une erreur imprévue s'est produite, si le problème perciste contacter le service informatique";
            _logRepository.LogErreur("Une erreut imprévu s'est produite !", ex);

            GetListShop();
            GetListTransports();
        }

        return result;
    }

    #endregion

    #region Méthodes privées
    private async Task<bool> CreateOrder(IdentityUser identityUser)
    {
        List<LineOrderDto> lineOrderDtos = new List<LineOrderDto>();

        foreach (CardItem item in listCardItems)
        {
            lineOrderDtos.Add(new LineOrderDto
            {
                HeaderOrderId = Guid.NewGuid(),
                QuantitySupplied = 0,
                Quantity = item.Quantity,
                ProductId = new Guid(item.IdProduct)
            });
        }
       
        Customer customer = _customerRepository.GetAddress(identityUser.Email).Result;

        CreateOrderDto createOrderDto = new CreateOrderDto 
        {
            Status = 0,
            Date = DateTime.Now,
            Paid = true,
            CustomerId = customer.Id,
            LineOrders = lineOrderDtos
        };

        return await _headerOrderRepository.CreateOrder(createOrderDto);
    }
    private void GetListTransports()
    {
        listTransports = _transportRepository.GetAllActiveTransports().Result;

        listSelectedTransports.Add(new SelectListItem
        {
            Text = "Veuillez choisir un transport",
            Value = Convert.ToString("-1")
        }); ;

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

    public MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
    public List<CardItem> listCardItems { get; set; } = new();
    public List<Product> listProducts { get; set; } = new();

    public List<SelectListItem> listSelectedTransports { get; set; } = new List<SelectListItem>();
    public Guid Id { get; set; }
    public List<Transport> listTransports { get; set; } = new List<Transport>();
    [BindProperty] public Transport transport { get; set; } = new();

    public string Token { get; } = TokenManager.Instance.GetToken();
    public int nbProduits;
    public double prixTotal;
    public int nbProduitsTotal;

    #endregion
}