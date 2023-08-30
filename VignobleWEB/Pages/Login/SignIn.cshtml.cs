using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VignobleWEB.Pages.Login
{
    public class SignInModel : PageModel
    {

        public SignInModel()
        {
        }

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            return result;
        }
    }
}