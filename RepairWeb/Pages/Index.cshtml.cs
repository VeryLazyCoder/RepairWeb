using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;

namespace RepairWeb.Pages
{
    public class IndexModel : PageModel
    {
        public bool IsUserAdminCandidate;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToPageByClaim();

            return Page();
        }

        private IActionResult RedirectToPageByClaim()
        {
            if (User.HasClaim(Claims.UserRole, "исполнитель"))
                return RedirectToPage("Executor");
            if (User.HasClaim(Claims.UserRole, "клиент"))
                return RedirectToPage("Client");
            if (User.HasClaim(c => c.Type == Claims.AdminCandidate))
            {
                IsUserAdminCandidate = true;
                return Page();
            }

            return RedirectToPage("Admin");
        }
    }
}
