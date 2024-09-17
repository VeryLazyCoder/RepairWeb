using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;

namespace RepairWeb.Pages
{
    [Authorize(Policies.IsAdmin)]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
