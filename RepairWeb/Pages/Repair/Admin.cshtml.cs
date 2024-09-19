using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data;

namespace RepairWeb.Pages.Repair
{
    [Authorize(Policies.IsAdmin)]
    public class AdminModel : PageModel
    {
        private UserManager<ApplicationUser> _userManager;

        public List<ApplicationUser> Candidates { get;}
        public AdminModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
