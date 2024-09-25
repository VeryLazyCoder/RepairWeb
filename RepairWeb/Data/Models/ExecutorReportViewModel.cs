namespace RepairWeb.Data.Models
{
    public class ExecutorReportViewModel
    {
        public string Id { get; set; }
        public int Cost { get; set; }
        public string Comment { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public string Equipment { get; set; }
        public string SerialNumber { get; set; }
    }
}
