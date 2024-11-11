namespace RepairWeb.Data.Entities
{
    public class Executor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float AverageRating { get; set; }
        public List<Request> Requests { get; set; } = new List<Request>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
