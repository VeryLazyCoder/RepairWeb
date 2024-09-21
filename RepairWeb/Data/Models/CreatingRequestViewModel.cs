using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RepairWeb.Data.Models
{
    public record CreatingRequestViewModel
    {
        public string? ClientId { get; set; }
        [Required]
        [Display(Name = "Неисправное оборудование")]
        public string Equipment { get; set; }
        [Required]
        [Display(Name = "Описание вашей проблемы")]
        public string ProblemDescription { get; set; }
        [Required]
        [Display(Name = "Серийный номер")]
        public string SerialNumber { get; set; }
        public string Status { get; set; } = RequestStatus.Init;
    }
}

