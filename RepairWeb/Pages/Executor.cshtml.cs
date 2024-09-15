using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;

namespace RepairWeb.Pages
{
    [Authorize(Policies.IsExecutor)]
    public class ExecutorModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
