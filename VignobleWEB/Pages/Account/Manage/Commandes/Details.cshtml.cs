using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Account.Manage.Commandes
{
    public class DetailsModel : PageModel
    {
        #region Champs
        private readonly IHeaderOrderRepository _headerOrderRepository;
        private readonly ILineOrderRepository _lineOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public DetailsModel(IHeaderOrderRepository headerOrderRepository, ILineOrderRepository lineOrderRepository, ICustomerRepository customerRepository, IPictureRepository pictureRepository, UserManager<IdentityUser> userManager, ILogRepository logRepository)
        {
            _headerOrderRepository = headerOrderRepository;
            _lineOrderRepository = lineOrderRepository;
            _customerRepository = customerRepository;
            _pictureRepository = pictureRepository;
            _userManager = userManager;
            _logRepository = logRepository;
        }
        #endregion
        public async Task<IActionResult> OnGet()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
                }

                Guid id = new Guid(HttpContext.Request.RouteValues["idCommande"].ToString());

                await getOrder(id);
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

            return Page();
        }

        #region Méthodes privées
        private async Task getOrder(Guid idHeaderOrder)
        {
            headerOrder = await _headerOrderRepository.GetHeaderOrderById(idHeaderOrder);

            linesOrder = await _lineOrderRepository.GetLinesOrderByHeaderOrder(idHeaderOrder);

            //Faut récup les img des produits

            foreach (LineOrder line in linesOrder)
            {
                line.Product.Picture = await _pictureRepository.GetImageById(line.Product.PictureId);
            }

            statusOrders = StatusOrder.getList();
        }
        #endregion

        #region Propriétées
        public HeaderOrder headerOrder { get; set; } = new();
        public List<LineOrder> linesOrder { get; set; } = new();
        public List<StatusOrder> statusOrders { get; set; } = new();
        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
        #endregion
    }
}
