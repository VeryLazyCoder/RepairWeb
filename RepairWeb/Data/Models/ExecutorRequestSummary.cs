namespace RepairWeb.Data.Models
{
    public record ExecutorRequestSummary
    {
        public string RequestId { get; set; }
        public string ProblemDescription { get; set; }
        public string Equipment { get; set; }
        public string SerialNumber { get; set; }
        public string Status { get; set; }
    }
}
