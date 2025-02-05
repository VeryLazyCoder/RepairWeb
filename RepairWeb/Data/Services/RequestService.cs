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

        public async Task<List<RequestSummaryModel>> GetRequestsSummary(ApplicationUser user)
        {
            return await _context.Requests.Where(r => r.ClientId == user.Id)
                .Include(r => r.Review)
                .OrderBy(r => r.RequestDate)
                .Select(x => new RequestSummaryModel(x.Id, x.Equipment, x.RequestDate, x.Status, x.Review))
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
                RequestDate = DateTime.Now,
            };

            await _context.Requests.AddAsync(request);

            await _context.SaveChangesAsync();
            return request.Id.ToString();
        }

        public async Task<ClientRequestViewModel> GetClientsRequestModel(string id)
        {
            var request = await _context.Requests.Where(x => x.Id.ToString() == id)
                .Include(r => r.Executor)
                    .SingleOrDefaultAsync();
            if (request == default)
                return null;
            
            var result = new ClientRequestViewModel
            {
                SerialNumber = request.SerialNumber,
                Status = request.Status,
                Equipment = request.Equipment,
                ProblemDescription = request.ProblemDescription,
                RequestId = id,
                ExecutorComment = request.ExecutorComment ?? "мастер не оставил никаких комментариев",
                ExecutorName = "мастер ещё не назначен",
                RequestDate = request.RequestDate,
                FulfillDate = request.FulfillDate,
                ExecutorAvatarPath = "/images/defaultUser.png",
            };

            if (request.ExecutorId != null)
            {
                var executorAvatarPath = (await _userManager.FindByIdAsync(request.ExecutorId))
                    ?.ProfileImageURL;
                result.ExecutorAvatarPath = string.IsNullOrEmpty(executorAvatarPath)
                    ? "/images/defaultUser.png" : executorAvatarPath;
                result.ExecutorRating = request.Executor.AverageRating;
                result.ExecutorName = request.Executor.Name;
                result.ExecutorAssigned = true;
            }
            return result;
        }

        public async Task<Request?> GetRequest(string id)
        {
            return await _context.Requests
                .Where(r => r.Id.ToString() == id)
                .Include(r => r.Executor)
                .FirstOrDefaultAsync();
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
