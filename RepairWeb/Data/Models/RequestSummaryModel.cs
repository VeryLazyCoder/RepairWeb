using RepairWeb.Data.Entities;

namespace RepairWeb.Data.Models
{
    public record RequestSummaryModel(Guid Id, string Equipment, 
        DateTime RequestDate, string Status, Review Review);
}
