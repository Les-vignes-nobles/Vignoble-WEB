using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Account.Manage.Commandes
{
    public class IndexModel : PageModel
    {
        #region Champs
        private readonly IHeaderOrderRepository _headerOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public IndexModel(IHeaderOrderRepository headerOrderRepository, ICustomerRepository customerRepository, UserManager<IdentityUser> userManager, ILogRepository logRepository)
        {
            _headerOrderRepository = headerOrderRepository;
            _customerRepository = customerRepository;
            _userManager = userManager;
            _logRepository = logRepository;
        }
        #endregion

        #region Méthodes publiques
        public async Task<IActionResult> OnGet()
        {
            IActionResult result = Page();

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
                }

                await RecupListeCommande(user);
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
        private async Task RecupListeCommande(IdentityUser identityUser)
        {
            Customer customer = _customerRepository.GetAddress(identityUser.Id).Result; 

            ListeEnteteCommande = _headerOrderRepository.RecupererListeEnteteCommandeDunClient(customer.Id).Result;
            
        }
        #endregion

        #region Propriétés
        [TempData]
        public string StatusMessage { get; set; }

        public List<HeaderOrder> ListeEnteteCommande { get; set; } = new List<HeaderOrder>();

        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
        #endregion
    }
}
