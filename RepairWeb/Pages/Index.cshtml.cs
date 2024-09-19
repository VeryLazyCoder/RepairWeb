using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data;
using RepairWeb.Data.Entities;

namespace RepairWeb.Pages
{
    public class IndexModel : PageModel
    {
        public bool IsUserAdminCandidate;
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                return RedirectToPage("Repair/Executor");
            if (User.HasClaim(Claims.UserRole, "клиент"))
                return RedirectToPage("Repair/Client");
            if (User.HasClaim(c => c.Type == Claims.AdminCandidate))
            {
                IsUserAdminCandidate = true;
                return Page();
            }

            return RedirectToPage("Repair/Admin");
        }
    }
}
