namespace RepairWeb.Data.Models
{
    public class AdminReportModel
    {
        public string ExecutorName { get; set; }
        public string ClientName { get; set; }
        public string ClientsDescription { get; set; }
        public string ExecutorComment { get; set; }
        public string Equipment { get; set; }
        public int Cost { get; set; }
        public DateTime FulfillDate { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
}
