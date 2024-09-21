using System.Security.Claims;
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
    [Authorize(Policies.IsAdmin)]
    public class AdminModel : PageModel
    {
        private UserManager<ApplicationUser> _userManager;
        private ExecutorRequestService _service;

        public List<ApplicationUser> Candidates { get;}
        public List<AdminRequestModel> Requests { get; set; }
        public AdminModel(UserManager<ApplicationUser> userManager, ExecutorRequestService service)
        {
            _userManager = userManager;
            _service = service;
            Candidates = new List<ApplicationUser>();
        }

        public async Task OnGet()
        {
            var users = _userManager.Users.ToList();
            
            foreach (var user in users)
            {
                var claims = await _userManager.GetClaimsAsync(user);

                if (claims.Any(c => c.Type == Claims.AdminCandidate))
                    Candidates.Add(user);
            }

            Requests = await _service.GetRequests();
        }

        public async Task<IActionResult> OnPostPromote(string candidateId)
        {
            var user = await _userManager.FindByIdAsync(candidateId);
            var result = await _userManager.AddClaimAsync(user, new Claim(Claims.UserRole, "admin"));
            await RemoveCandidateClaim(user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostReject(string candidateId)
        {
            var user = await _userManager.FindByIdAsync(candidateId);
            var result = await _userManager.AddClaimAsync(user, new Claim(Claims.UserRole, "клиент"));
            await RemoveCandidateClaim(user);
            return RedirectToPage();
        }

        private async Task RemoveCandidateClaim(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            await _userManager.RemoveClaimAsync(user, claims.FirstOrDefault(c => c.Type == Claims.AdminCandidate));
        }
    }
}
