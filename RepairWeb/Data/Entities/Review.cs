namespace RepairWeb.Data.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateOnly ReviewDate { get; set; }
        public string ExecutorId { get; set; }
        public Request Request { get; set; }
        public Executor Executor { get; set; }
    }
}
