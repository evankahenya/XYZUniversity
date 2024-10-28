using XYZUniversity.API.Models.Domain;

namespace XYZUniversity.API.Models.DTO
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }               // Student's name
        public string CourseName { get; set; }          // Student's Course Name
        public string EnrollmentStatus { get; set; }    // Student's Enrollment Status

        // Navigation property for payments
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
