using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Models
{
    public record AdminRequestModel
    {
        public string Equipment { get; set; }
        public string ProblemDescription { get; set; }
        public string RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public Executor Executor { get; set; }
    }
}
