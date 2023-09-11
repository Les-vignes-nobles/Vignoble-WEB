using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Interfaces.Infrastructure.Tools;

namespace VignobleWEB.Pages.Login
{
    public class SignUpModel : PageModel
    {
        #region Champs
        private readonly ILogRepository _logTools;

        #endregion

        #region Constructeur
        public SignUpModel(ILogRepository logTools)
        {
            _logTools = logTools;
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
                _logTools.LogErreur("Une erreur s'est produite lors du GET sur la page index des r�f�rences (Liste) !", ex);
            }

            return result;
        }

        #endregion

        #region M�thodes priv�es
        
        #endregion

        #region Propriétés

        #endregion
    }
}