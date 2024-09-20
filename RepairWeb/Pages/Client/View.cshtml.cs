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

        public IActionResult OnPostDelete(string id)
        {
            return RedirectToPage();
        }

        public void OnPost()
        {
            var a = Request.ProblemDescription;
            var b = Request.SerialNumber;
        }
    }
}
