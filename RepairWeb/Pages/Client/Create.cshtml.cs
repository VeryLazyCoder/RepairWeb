using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data;
using RepairWeb.Data.Models;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.Client
{
    [Authorize(Policies.IsClient)]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreatingRequestViewModel Input { get; set; }
        private UserManager<ApplicationUser> _userManager;
        private RequestService _requestService;

        public CreateModel(RequestService requestService, UserManager<ApplicationUser> userManager)
        {
            _requestService = requestService;
            _userManager = userManager;
        }

        public void OnGet()
        {
            Input = new CreatingRequestViewModel();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                Input.ClientId = user.Id;
                await _requestService.CreateRequest(Input);
                return RedirectToPage("Repair/Client");
            }

            return Page();
        }
    }
}
