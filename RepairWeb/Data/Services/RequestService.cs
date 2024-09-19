using Microsoft.EntityFrameworkCore;
using RepairWeb.Data.Entities;
using RepairWeb.Data.Models;

namespace RepairWeb.Data.Services
{
    public class RequestService
    {
        private readonly ApplicationDbContext _context;

        public RequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestSummaryViewModel>> GetRequestsSummary(ApplicationUser user)
        {
            return await _context.Requests.Where(r => r.ClientId == user.Id)
                .Select(x => new RequestSummaryViewModel(x.Id, x.Equipment, x.Status))
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
    }
}
