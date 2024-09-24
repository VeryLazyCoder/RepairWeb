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
                .Where(r => r.ExecutorId == executorId)
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
    }
}
