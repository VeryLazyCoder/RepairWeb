namespace RepairWeb.Data.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public Request Request { get; set; }
        public Guid RequestId { get; set; }
        public string ExecutorId { get; set; }
        public DateTime CreatingDate { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public int Cost { get; set; }
        public string Comments { get; set; }
    }
}
