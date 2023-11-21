using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        #region Champs
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public PersonalDataModel(UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger, ILogRepository logRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _logRepository = logRepository;
        }
        #endregion

        #region Méthodes publiques
        public async Task<IActionResult> OnGet()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
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

            return Page();
        }
        #endregion

        #region Propriétés
        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };
        #endregion
    }
}
