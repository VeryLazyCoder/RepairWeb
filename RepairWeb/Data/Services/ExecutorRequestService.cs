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
            var request = _context.FindAsync<Request>(requestId).Result;
            var executor = _context.FindAsync<Executor>(executorId).Result;

            executor.Requests.Add(request);
            request.Executor = executor;

            await _context.SaveChangesAsync();
        }
    }
}
