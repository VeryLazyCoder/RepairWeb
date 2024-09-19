using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;

namespace RepairWeb.Pages.Repair
{
    [Authorize(Policies.IsClient)]
    public class ClientModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
