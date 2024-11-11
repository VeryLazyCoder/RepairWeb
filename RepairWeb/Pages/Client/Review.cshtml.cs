using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using RepairWeb.Authorization;
using RepairWeb.Data.Entities;
using RepairWeb.Data.Services;
using RequestEntity = RepairWeb.Data.Entities.Request;

namespace RepairWeb.Pages.Client
{
    [Authorize(Policies.IsClient)]
    public class ReviewModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public RequestEntity Request { get; private set; }

        private ReviewService _reviewService;
        private RequestService _requestService;

        public ReviewModel(ReviewService reviewService, RequestService requestService)
        {
            _reviewService = reviewService;
            _requestService = requestService;
        }

        public class InputModel
        {
            [Required]
            public string Comment { get; set; }
            [Required]
            public int Rating { get; set; }
        }

        public async Task<IActionResult> OnGet(string id)
        {
            Input = new InputModel();

            Request = await _requestService.GetRequest(id);
            if (Request == null)
                return NotFound();
            if (Request.Review != null)
                return Forbid();
            return Page();
        }

        public async Task<IActionResult> OnPost(string requestId)
        {
            var request = await _requestService.GetRequest(requestId);
            await _reviewService.CreateReview(request, Input.Rating, Input.Comment);
            return RedirectToPage("/Repair/Client");
        }
    }
}
