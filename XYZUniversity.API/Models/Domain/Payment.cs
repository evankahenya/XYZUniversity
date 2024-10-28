namespace XYZUniversity.API.Models.Domain
{
    public class Payment
    {
        public Guid Id { get; set; }          // Unique identifier for the payment
        public Guid StudentId { get; set; }   // Foreign key reference to the student
        public decimal Amount { get; set; }   // Amount of the payment
        public DateTime PaymentDate { get; set; } // Date of the payment

    }
}
