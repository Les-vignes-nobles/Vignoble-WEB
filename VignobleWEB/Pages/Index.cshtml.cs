using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VignobleWEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //Création d'une variable de session pour test
            HttpContext.Session.SetString("panier", "0/1/3/5");
        }
    }
}