namespace RepairWeb.Data.Models
{
    public static class RequestStatus
    {
        public const string Init = "В обработке";
        public const string Processing = "Ремонтируется";
        public const string AwaitingSpareParts = "Ждём нужных запчастей";
        public const string AwaitingPayment = "Требуется оплата";
        public const string Fulfill = "Заявка закрыта";
    }
}
