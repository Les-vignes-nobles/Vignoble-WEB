using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VignobleWEB.Pages.Login
{
    public class SignUpModel : PageModel
    {

        public SignUpModel()
        {
        }

        public IActionResult OnGet()
        {
            IActionResult result = Page();

            return result;
        }
    }
}