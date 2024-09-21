using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairWeb.Data.Entities;
using RepairWeb.Data.Models;

namespace RepairWeb.Data.Services
{
    public class RequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<RequestSummaryViewModel>> GetRequestsSummary(ApplicationUser user)
        {
            return await _context.Requests.Where(r => r.ClientId == user.Id)
                .Select(x => new RequestSummaryViewModel(x.Id, x.Equipment, x.RequestDate, x.Status))
                .ToListAsync();
        }

        public async Task<string> CreateRequest(CreatingRequestViewModel model)
        {
            var request = new Request
            {
                ProblemDescription = model.ProblemDescription,
                ClientId = model.ClientId,
                Equipment = model.Equipment,
                SerialNumber = model.SerialNumber,
                Status = model.Status,
            };

            await _context.Requests.AddAsync(request);

            await _context.SaveChangesAsync();
            return request.Id.ToString();
        }

        public async Task<ClientRequestViewModel> GetRequest(string id)
        {
            var request = await _context.Requests.Where(x => x.Id.ToString() == id)
                    .SingleOrDefaultAsync();
            if (request == default)
                return null;
            var executorName = request.ExecutorId == default ? "мастер ещё не назначен" :
                    _userManager.FindByIdAsync(request.ExecutorId.ToString()).Result.FullName;
            return new ClientRequestViewModel
            {
                SerialNumber = request.SerialNumber,
                Status = request.Status,
                Equipment = request.Equipment,
                ProblemDescription = request.ProblemDescription,
                RequestId = id,
                ExecutorComment = request.ExecutorComment ?? "мастер не оставил никаких комментариев",
                ExecutorName = executorName,
                RequestDate = request.RequestDate,
                FulfillDate = request.FulfillDate
            };
        }

        public async Task UpdateRequest(ClientRequestViewModel model, string id)
        {
            await _context.Requests
                .Where(r => r.Id.ToString() == id)
                .ExecuteUpdateAsync(req =>
                    req.SetProperty(p => p.ProblemDescription, model.ProblemDescription)
                        .SetProperty(p => p.SerialNumber, model.SerialNumber));
        }

        public async Task DeleteRequest(string id)
        {
            await _context.Requests
                .Where(r => r.Id.ToString() == id)
                .ExecuteDeleteAsync();
        }
    }
}
