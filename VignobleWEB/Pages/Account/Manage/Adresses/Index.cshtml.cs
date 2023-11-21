using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Account.Manage.Adresses
{
    public class IndexModel : PageModel
    {
        #region Champs
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public IndexModel(ICustomerRepository customerRepository, UserManager<IdentityUser> userManager, ILogRepository logRepository)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
            _logRepository = logRepository;
        }
        #endregion

        #region Méthodes publiques
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
                }

                await GetAdress(user);
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
        #endregion

        #region Méthodes privées
        private async Task GetAdress(IdentityUser user) 
        {
            var mail = await _userManager.GetEmailAsync(user);
            Customer = _customerRepository.GetAddress(mail).Result;
        }
        #endregion

        #region Propriétés
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty] public Customer Customer { get; set;} = new Customer();

        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
        #endregion
    }
}
