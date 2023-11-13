using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace VignobleWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        #region Champs
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        #endregion

        #region Constructeur
        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        #endregion

        #region Priopriétés
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        #endregion

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

        #region Méthodes publiques
        public async Task OnGetAsync(string returnUrl = null)
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

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("/Index");

            if (ModelState.IsValid)
            {

                //lockoutOnFailure: true pour bloquer le compte au bout d'un certain nombre de tentative
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Utilisateur '{Input.Email}' connecté");
                    return LocalRedirect(returnUrl);
                }
                
                if (result.IsLockedOut)
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

            return Page();
        }
        #endregion
    }
}
