using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using RepairWeb.Authorization;
using RepairWeb.Data.Entities;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.Client
{
    [Authorize(Policies.IsClient)]
    public class ReviewModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        private Request _request;
        private ReviewService _service;

        public ReviewModel(ReviewService service)
        {
            _service = service;
        }

        public class InputModel
        {
            [Required]
            public string Comment { get; set; }
            [Required]
            public int Rating { get; set; }
        }

        public async Task OnGet(string id)
        {
            Input = new InputModel();
        }

        public async Task OnPost()
        {

        }
    }
}
