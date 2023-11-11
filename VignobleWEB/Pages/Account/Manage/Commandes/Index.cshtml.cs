using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VignobleWEB.Core.Interfaces.Application.Repositories;
using VignobleWEB.Core.Models;

namespace VignobleWEB.Pages.Account.Manage.Commandes
{
    public class IndexModel : PageModel
    {
        #region Champs
        private readonly IHeaderOrderRepository _headerOrderRepository;
        #endregion

        #region Constructeur
        public IndexModel(IHeaderOrderRepository headerOrderRepository)
        {
            _headerOrderRepository = headerOrderRepository;
        }
        #endregion

        #region M�thodes publiques
        public IActionResult OnGet()
        {
            IActionResult result = Page();

            RecupListeCommande();

            return result;
        }
        #endregion

        #region M�thodes priv�es
        private void RecupListeCommande()
        {
            Guid id;

            ListeEnteteCommande = _headerOrderRepository.RecupererListeEnteteCommandeDunClient(id).Result;
        }
        #endregion

        #region Propri�t�s
        [TempData]
        public string StatusMessage { get; set; }

        public List<HeaderOrder> ListeEnteteCommande { get; set; } = new List<HeaderOrder>();
        #endregion
    }
}
