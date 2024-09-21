namespace RepairWeb.Data.Models
{
    public class ClientRequestViewModel
    {
        public string ExecutorName { get; set; }
        public string RequestId { get; set; }
        public string Equipment { get; set; }
        public string ExecutorComment { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }
        public string SerialNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime FulfillDate { get; set; }
    }
}
