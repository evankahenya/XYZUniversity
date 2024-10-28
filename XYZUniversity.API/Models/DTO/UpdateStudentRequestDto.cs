using System.ComponentModel.DataAnnotations;

namespace XYZUniversity.API.Models.DTO
{
    public class UpdateStudentRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Student name exceeds allocated length")]
        public string Name { get; set; }               // Student's name
        [Required]
        [MaxLength(100, ErrorMessage = "Course name exceeds allocated length")]
        public string CourseName { get; set; }          // Student's Course Name
        [Required]
        [MaxLength(10, ErrorMessage = "Enrollment Status name exceeds allocated length")]
        public string EnrollmentStatus { get; set; }    // Student's Enrollment Status
    }
}
