using System.ComponentModel.DataAnnotations;
using XYZUniversity.API.Models.Domain;

namespace XYZUniversity.API.Models.DTO
{
    public class AddPaymentRequestDto
    {
        [Required]
     
        public Guid StudentId { get; set; }   // Foreign key reference to the student
        [Required]
        [Range(1, 999999.99, ErrorMessage = "Amount must be between 1 and 999999.99")]
        public decimal Amount { get; set; }   // Amount of the payment
        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; } // Date of the payment

    }
}
