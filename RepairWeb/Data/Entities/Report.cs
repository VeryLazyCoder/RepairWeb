using System.ComponentModel.DataAnnotations.Schema;

namespace RepairWeb.Data.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public Request Request { get; set; }
        public Guid RequestId { get; set; }
        public string ExecutorId { get; set; }
        public DateTime CreatingDate { get; set; }
        public int Cost { get; set; }
        public string Comments { get; set; }
        public int DurationDays { get; set; }
        public TimeSpan DurationTime { get; set; }

        [NotMapped]
        public TimeSpan TimeSpent
        {
            get => new TimeSpan(DurationDays, DurationTime.Hours, DurationTime.Minutes, DurationTime.Seconds);
            set
            {
                DurationDays = value.Days;
                DurationTime = new TimeSpan(0, value.Hours, value.Minutes, value.Seconds);
            }
        }
    }
}
