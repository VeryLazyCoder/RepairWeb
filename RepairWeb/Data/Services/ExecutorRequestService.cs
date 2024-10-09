using Microsoft.EntityFrameworkCore;
using RepairWeb.Data.Entities;
using RepairWeb.Data.Models;

namespace RepairWeb.Data.Services
{
    public class ExecutorRequestService
    {
        private readonly ApplicationDbContext _context;

        public ExecutorRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Executor>> GetExecutors()
        {
            return await _context.Executors.ToListAsync();
        }

        public async Task<List<AdminRequestModel>> GetRequests()
        {
            return await _context.Requests
                .Where(r => r.ExecutorId == null)
                .Select(r => new AdminRequestModel()
                {
                    Equipment = r.Equipment,
                    ProblemDescription = r.ProblemDescription,
                    RequestDate = r.RequestDate,
                    RequestId = r.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task UpdateRequestsExecutor(string requestId, string executorId)
        {
            var request = _context.Requests.FirstOrDefault(r => r.Id.ToString() == requestId);
            var executor = _context.FindAsync<Executor>(executorId).Result;

            executor.Requests.Add(request);
            request.Executor = executor;

            await _context.SaveChangesAsync();
        }

        public async Task<List<ExecutorRequestSummary>> GetRequestsSummary(string executorId)
        {
            return await _context.Requests
                .Where(r => r.ExecutorId == executorId && r.Report == null)
                .OrderBy(r => r.RequestDate)
                .Select(r => new ExecutorRequestSummary()
                {
                    ProblemDescription = r.ProblemDescription,
                    SerialNumber = r.SerialNumber,
                    Equipment = r.Equipment,
                    RequestId = r.Id.ToString(),
                    Status = r.Status
                })
                .ToListAsync();
        }

        public async Task<ExecutorRequestViewModel?> GetRequest(string id)
        {
            return await _context.Requests
                .Where(r => r.Id.ToString() == id)
                .Select(r => new ExecutorRequestViewModel
                {
                    Equipment = r.Equipment,
                    ExecutorComment = r.ExecutorComment,
                    Id = r.Id.ToString(),
                    Status = r.Status,
                    ProblemDescription = r.ProblemDescription,
                    RequestDate = r.RequestDate,
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateRequestStatus(string id, string comment, string status)
        {
            await _context.Requests
                .Where(r => r.Id.ToString() == id)
                .ExecuteUpdateAsync(r =>
                    r.SetProperty(p => p.ExecutorComment, comment)
                        .SetProperty(p => p.Status, status));

        }

        public async Task<string> CreateReport(ExecutorRequestViewModel requestModel)
        {
            var request = await _context.Requests
                .Where(r => r.Id.ToString() == requestModel.Id)
                .FirstOrDefaultAsync() ?? throw new NullReferenceException();

            await FulfillRequest(request);
            
            var report = new Report
            {
                Request = request,
                CreatingDate = DateTime.Now,
                ExecutorId = request.ExecutorId,
                TimeSpent = request.FulfillDate - request.RequestDate
            };
            await _context.Reports.AddAsync(report);
            request.Report = report;

            await _context.SaveChangesAsync();

            return report.Id.ToString();
        }

        private async Task FulfillRequest(Request requestModel)
        {
            var date = DateTime.Now;
            await _context.Requests.Where(r => r.Id == requestModel.Id)
                .ExecuteUpdateAsync(r =>
                    r.SetProperty(p => p.FulfillDate, date));

            requestModel.FulfillDate = date;
        }
    }
}
