namespace RepairWeb.Data.Entities
{
    public class Request
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public string ExecutorId { get; set; }
        public string Equipment { get; set; }
        public string SerialNumber { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }
        public string ExecutorComment { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime FulfillDate { get; set; }
        public Report Report { get; set; }
    }
}
