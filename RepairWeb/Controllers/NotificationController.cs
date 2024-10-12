using Microsoft.AspNetCore.Mvc;
using RepairWeb.Data.Services;

namespace RepairWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            await _notificationService.MarkNotificationAsRead(id);

            return Ok();
        }
    }
}
