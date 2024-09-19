using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;

namespace RepairWeb.Pages.Client
{
    [Authorize(Policies.IsClient)]
    public class ViewModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
