namespace XYZUniversity.API.Models.DTO
{
    public class UpdateStudentRequestDto
    {
        public string Name { get; set; }               // Student's name
        public string CourseName { get; set; }          // Student's Course Name
        public string EnrollmentStatus { get; set; }    // Student's Enrollment Status
    }
}
