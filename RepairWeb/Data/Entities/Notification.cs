using RepairWeb.Data.Models;

namespace RepairWeb.Data.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string RequestId { get; set; }
        public string ClientId { get; set; }
        public string Equipment { get; set; }
        public string Status { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }
        public bool IsRead { get; set; }
       
    }
}
