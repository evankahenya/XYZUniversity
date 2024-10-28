using System.ComponentModel.DataAnnotations;

namespace XYZUniversity.API.Models.DTO
{
    public class UpdatePaymentRequestDto
    {
        //public Guid Id { get; set; }          // Unique identifier for the payment
        //public Guid StudentId { get; set; }   // Foreign key reference to the student
        [Required]
        [Range(1, 999999.99, ErrorMessage = "Amount must be between 1 and 999999.99")]
        public decimal Amount { get; set; }   // Amount of the payment
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public DateTime PaymentDate { get; set; } // Date of the payment
    }
}
