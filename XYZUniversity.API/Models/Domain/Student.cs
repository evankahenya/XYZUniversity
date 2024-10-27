namespace XYZUniversity.API.Models.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public string EnrollmentStatus { get; set; }
    }
}
