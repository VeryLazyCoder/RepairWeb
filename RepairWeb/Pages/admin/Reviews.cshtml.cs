using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data.Entities;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.admin
{
    [Authorize(Policies.IsAdmin)]
    public class ReviewsModel : PageModel
    {
        public List<Review> Reviews { get; set; }
        private readonly ReviewService _reviewService;

        public ReviewsModel(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task OnGet()
        {
            Reviews = await _reviewService.GetLatestReviews(10);
        }
    }
}
