using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data.Models;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.Client
{
    [Authorize(Policies.IsClient)]
    public class ViewModel : PageModel
    {
        [BindProperty]
        public ClientRequestViewModel Request { get; set; }
        private RequestService _requestService;

        public ViewModel(RequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Request = await _requestService.GetRequest(id);

            if (Request == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            await _requestService.DeleteRequest(id);

            return RedirectToPage("/Repair/Client");
        }

        public async Task<IActionResult> OnPost(string id)
        {
            await _requestService.UpdateRequest(Request, id);

            return RedirectToPage("/Repair/Client");
        }
    }
}
