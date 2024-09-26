using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepairWeb.Authorization;
using RepairWeb.Data.Models;
using RepairWeb.Data.Services;

namespace RepairWeb.Pages.admin
{
    [Authorize(Policies.IsAdmin)]
    public class ReportsModel : PageModel
    {
        public List<AdminReportModel> Reports { get; private set; }

        private readonly ReportService _service;

        public ReportsModel(ReportService service)
        {
            _service = service;
        }

        public async Task OnGet()
        {
            Reports = await _service.GetReports();
        }
    }
}
