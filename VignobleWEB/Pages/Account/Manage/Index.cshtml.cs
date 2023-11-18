using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VignobleWEB.Core.Application.RepositoriesException;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        #region Champs
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogRepository _logRepository;
        #endregion

        #region Constructeur
        public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogRepository logRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

                await LoadAsync(user);
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

                if (!ModelState.IsValid)
                {
                    await LoadAsync(user);
                    return Page();
                }

                var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                if (Input.PhoneNumber != phoneNumber)
                {
                    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                    if (!setPhoneResult.Succeeded)
                    {
                        StatusMessage = "Erreur inattendue lors de la définition du numéro de téléphone.";
                        return RedirectToPage();
                    }
                }

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Votre profil a été mis à jour";
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

            return RedirectToPage();
        }
        #endregion

        #region Méthodes privées
        private async Task<IActionResult> LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };

            return Page();
        }
        #endregion

        #region Propriétés
        public string Username { get; set; }

        public Core.Models.Interne.MessageModal MessagePourLaModal { get; set; } = new() { Titre = "Une erreur s'est produite" };

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
        #endregion
    }
}
