using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VignobleWEB.Pages
{
    public class ArticlesModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public ArticlesModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}