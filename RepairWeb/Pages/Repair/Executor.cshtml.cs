using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data;
using RepairWeb.Data.Models;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.Repair
{
    [Authorize(Policies.IsExecutor)]
    public class ExecutorModel : PageModel
    {
        public List<ExecutorRequestSummary> Requests { get; set; }
        private readonly ExecutorRequestService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExecutorModel(ExecutorRequestService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            Requests = await _service.GetRequestsSummary(user.Id);
        }

        public string GetButtonText(string status)
        {
            return status == RequestStatus.Init ? "ѕриступить к ремонту" : "продолжить ремонт";
        }
    }
}
