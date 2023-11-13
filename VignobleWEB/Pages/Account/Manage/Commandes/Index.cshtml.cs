using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Account.Manage.Commandes
{
    public class IndexModel : PageModel
    {
        #region Champs
        private readonly IHeaderOrderRepository _headerOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructeur
        public IndexModel(IHeaderOrderRepository headerOrderRepository, ICustomerRepository customerRepository, UserManager<IdentityUser> userManager)
        {
            _headerOrderRepository = headerOrderRepository;
            _customerRepository = customerRepository;
            _userManager = userManager;
        }
        #endregion

        #region Méthodes publiques
        public async Task<IActionResult> OnGet()
        {
            IActionResult result = Page();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
            }

            await RecupListeCommande(user);

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
        #endregion
    }
}
