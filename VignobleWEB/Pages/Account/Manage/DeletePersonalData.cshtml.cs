using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VignobleWEB.Core.Interfaces.Application.Repositories;

namespace VignobleWEB.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        #region Champs
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IAccountRepository _accountRepository;
        #endregion

        #region Constructeur
        public DeletePersonalDataModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<DeletePersonalDataModel> logger, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _accountRepository = accountRepository;
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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Impossible de charger l'utilisateur avec l'ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
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
        #endregion
    }
}
