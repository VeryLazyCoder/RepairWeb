namespace RepairWeb.Data.Models
{
    public record CreatingRequestViewModel(string ClientId, string Equipment, string ProblemDescription,
        string SerialNumber, string Status = "в обработке");
}
