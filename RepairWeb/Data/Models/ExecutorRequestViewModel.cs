namespace RepairWeb.Data.Models
{
    public class ExecutorRequestViewModel
    {
        public string Id { get; set; }
        public string Equipment { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }
        public string ExecutorComment { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
