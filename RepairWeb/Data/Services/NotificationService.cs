using Microsoft.EntityFrameworkCore;
using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Services
{
    public class NotificationService
    {
        private ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<List<Notification>> GetNotifications(string clientId)
        {
            return await _context.Notifications
                .Where(n => n.ClientId == clientId && n.IsRead == false)
                .ToListAsync();
        }

        public async Task MarkNotificationAsRead(string id)
        {
            await _context.Notifications
                .Where(n => n.Id.ToString() == id)
                .ExecuteUpdateAsync(n => 
                    n.SetProperty(p => p.IsRead, true));
        }
    }
}
