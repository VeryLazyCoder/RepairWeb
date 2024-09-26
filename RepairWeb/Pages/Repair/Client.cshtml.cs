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
    [Authorize(Policies.IsClient)]
    public class ClientModel : PageModel
    {
        public List<RequestSummaryModel> Requests { get; set; }
        
        private UserManager<ApplicationUser> _userManager;
        private RequestService _requestService;

        public ClientModel(UserManager<ApplicationUser> userManager, RequestService requestService)
        {
            _userManager = userManager;
            _requestService = requestService;
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            Requests = [..await _requestService.GetRequestsSummary(user)];
        }
    }
}
