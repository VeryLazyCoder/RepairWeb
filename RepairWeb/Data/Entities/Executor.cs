namespace RepairWeb.Data.Entities
{
    public class Executor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Request> Requests { get; set; } = new List<Request>();
    }
}
