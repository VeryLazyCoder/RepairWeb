using Microsoft.EntityFrameworkCore;
using RepairWeb.Data.Models;

namespace RepairWeb.Data.Services
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ExecutorReportViewModel?> GetReport(string id)
        {
            return await _context.Reports
                .Where(r => r.Id.ToString() == id)
                .Include(r => r.Request)
                .Select(r => new ExecutorReportViewModel()
                {
                    Id = r.Id.ToString(),
                    TimeSpent = r.TimeSpent,
                    Equipment = r.Request.Equipment,
                    SerialNumber = r.Request.SerialNumber,
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateReport(ExecutorReportViewModel report)
        {
            await _context.Reports
                .Where(r => r.Id.ToString() == report.Id)
                .ExecuteUpdateAsync(r =>
                    r.SetProperty(p => p.Comments, report.Comment)
                        .SetProperty(p => p.Cost, report.Cost));
        }
    }
}
