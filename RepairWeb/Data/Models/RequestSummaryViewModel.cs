namespace RepairWeb.Data.Models
{
    public record RequestSummaryViewModel(Guid Id, string Equipment, DateTime RequestDate, string Status);
}
