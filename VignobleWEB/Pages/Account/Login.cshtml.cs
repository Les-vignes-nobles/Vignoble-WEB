using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        #region Champs
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, ILogRepository logRepository)
        {
            _signInManager = signInManager;
            _logger = logger;
            _logRepository = logRepository;
        }
        #endregion

        #region Méthodes publiques
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            IActionResult result = this.Page();

            try
            {
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                }

                returnUrl ??= Url.Content("/Index");

                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                ReturnUrl = returnUrl;
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

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            IActionResult result = this.Page();

            try
            {
                returnUrl ??= Url.Content("/Index");

                if (ModelState.IsValid)
                {

                    //lockoutOnFailure: true pour bloquer le compte au bout d'un certain nombre de tentative
                    var resultUser = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                    if (resultUser.Succeeded)
                    {
                        _logger.LogInformation($"Utilisateur '{Input.Email}' connecté");
                        return LocalRedirect(returnUrl);
                    }

                    if (resultUser.IsLockedOut)
                    {
                        _logger.LogWarning("Compte utilisateur bloqué.");
                        return RedirectToPage("Account/Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Mot de passe ou mail incorrect !");
                        return Page();
                    }
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

            return result;
        }
        #endregion

        #region Priopriétés
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Se souvenir de moi ?")]
            public bool RememberMe { get; set; }
        }
        #endregion
    }
}
