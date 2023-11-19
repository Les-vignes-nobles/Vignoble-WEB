using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        #region Champs
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public DeletePersonalDataModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<DeletePersonalDataModel> logger, IAccountRepository accountRepository, ILogRepository logRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _accountRepository = accountRepository;
            _logRepository = logRepository;
        }
        #endregion

        #region Propriétés
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }
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

                RequirePassword = await _userManager.HasPasswordAsync(user);
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
                }

                RequirePassword = await _userManager.HasPasswordAsync(user);
                if (RequirePassword)
                {
                    if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                    {
                        ModelState.AddModelError(string.Empty, "Mot de passe incorrect");
                        return Page();
                    }
                }

                var result = await _userManager.DeleteAsync(user);
                var userId = await _userManager.GetUserIdAsync(user);
                await _accountRepository.DeleteUser(userId);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Une erreur inattendue s'est produite lors de la suppression de l'utilisateur.");
                }

                await _signInManager.SignOutAsync();

                _logger.LogInformation("L'utilisateur avec l'ID '{UserId}' s'est supprimé lui-même.", userId);

                return Redirect("/Index");
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

