using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Account.Manage.Adresses
{
    public class IndexModel : PageModel
    {
        #region Champs
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructeur
        public IndexModel(ICustomerRepository customerRepository, UserManager<IdentityUser> userManager)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
        }
        #endregion

        #region Méthodes publiques
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
            }

            await GetAdress(user);
            return Page();
        }
        #endregion

        #region Méthodes privées
        private async Task GetAdress(IdentityUser user) 
        {
            var id = await _userManager.GetUserIdAsync(user);
            _customerRepository.GetAdress(Convert.ToInt32(id));
        }
        #endregion

        #region Propriétés
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty] public Customer Customer { get; set;} = new Customer();
        #endregion
    }
}
