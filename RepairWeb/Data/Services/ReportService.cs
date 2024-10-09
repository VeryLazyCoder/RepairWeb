using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairWeb.Data.Models;

namespace RepairWeb.Data.Services
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<List<AdminReportModel>> GetReports()
        {
            return await _context.Reports
                .Include(r => r.Request)
                .ThenInclude(r => r.Executor)
                .Select(r => new AdminReportModel()
                {
                    FulfillDate = r.Request.FulfillDate,
                    Equipment = r.Request.Equipment,
                    ExecutorComment = r.Comments,
                    Cost = r.Cost,
                    TimeSpent = r.TimeSpent,
                    ClientsDescription = r.Request.ProblemDescription,
                    ExecutorName = r.Request.Executor.Name,
                    ClientName = _userManager.Users
                        .Where(u => u.Id == r.Request.ClientId)
                        .FirstOrDefault().FullName
                })
                .ToListAsync();
        }
    }
}
