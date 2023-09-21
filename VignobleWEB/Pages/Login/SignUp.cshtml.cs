using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;
using VignobleWEB.Core.Models;
using VignobleWEB.Properties;

namespace VignobleWEB.Pages.Login
{
    public class SignUpModel : PageModel
    {
        #region Champs
        private readonly ILogRepository _logTools;
        private readonly IAccountRepository _accountRepository;

        #endregion

        #region Constructeur
        public SignUpModel(ILogRepository logTools, IAccountRepository accountRepository)
        {
            _logTools = logTools;
            _accountRepository = accountRepository;
        }
        #endregion

        #region M�thodes publique

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            try
            {
               
            }
            catch (Exception ex)
            {
                _logTools.LogErreur("Une erreur s'est produite lors du GET sur la page de création de compte !", ex);
            }

            return result;
        }

        public IActionResult OnPost()
        {
            IActionResult result = Page();

            try
            {
                HashPassword();

                if (_accountRepository.CreateUser(user, customer))
                {
                    return RedirectToPage("/Index");
                }
            }
            catch (Exception ex)
            {

                _logTools.LogErreur("Une erreur s'est produite lors du POST sur la page de création de compte !", ex);
            }
            return result;
        }

        #endregion

        #region M�thodes priv�es

        private void HashPassword()
        {
            byte[] salt;
            user.EncryptPassword = EncryptPassword(password, out salt);
            user.PasswordSalt = salt;
        }

        private string EncryptPassword(string password, out byte[] salt)
        {
            salt = new byte[Constantes.SALT_LENGTH];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        #endregion

        #region Propriétés
        [BindProperty]
        public User user { get; set; } = new User();

        [BindProperty]
        public Customer customer { get; set; } = new();

        [BindProperty]
        public string password { get; set; } = string.Empty;

        [BindProperty]
        public string passwordVerified { get; set; } = string.Empty;

        #endregion
    }
}