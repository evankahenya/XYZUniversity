using XYZUniversity.API.Models.Domain;

namespace XYZUniversity.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();                  // Retrieve all students
        Task<Student?> GetByIdAsync(Guid id);               // Retrieve a student by ID
        Task<Student> CreateAsync(Student student);          // Create a new student
        Task<Student?> UpdateAsync(Guid id, Student student); // Update an existing student
        Task<Student?> DeleteAsync(Guid id);                 // Delete a student by ID
    }
}
