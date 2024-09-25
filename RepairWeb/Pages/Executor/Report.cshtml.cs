using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data.Models;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.Executor
{
    [Authorize(Policies.IsExecutor)]
    public class ReportModel : PageModel
    {
        [BindProperty]
        public ExecutorReportViewModel Report { get; set; }

        private ReportService _service;

        public ReportModel(ReportService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var report = await _service.GetReport(id);

            if (report == null)
                return NotFound();

            Report = report;
            return Page();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            Report.Id = id;

            await _service.UpdateReport(Report);

            return RedirectToPage("/Repair/Executor");
        }
    }
}
