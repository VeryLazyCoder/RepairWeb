using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data.Models;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.Executor
{
    [Authorize(Policies.IsExecutor)]
    public class ViewModel : PageModel
    {
        [BindProperty]
        public ExecutorRequestViewModel Request { get; set; }
        private ExecutorRequestService _service;

        public ViewModel(ExecutorRequestService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Request = await _service.GetRequest(id);

            if (Request == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostReportAsync(string id)
        {
            Request.Id = id;
            var reportId = await _service.CreateReport(Request);

            return RedirectToPage("/Executor/Report", new {id = reportId});
        }

        public async Task<IActionResult> OnPost(string id)
        {
            await _service.UpdateRequestStatus(id, Request.ExecutorComment, Request.Status);
            return RedirectToPage("/Repair/Executor");
        }

        public List<string> GetStatusValues()
        {
            return typeof(RequestStatus)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null).ToString())
                .ToList();
        }
    }
}
