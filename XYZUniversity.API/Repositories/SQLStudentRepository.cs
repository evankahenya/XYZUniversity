using Microsoft.EntityFrameworkCore;
using XYZUniversity.API.Data;
using XYZUniversity.API.Models.Domain;
using XYZUniversity.API.Repositories;

namespace XYZUniversity.API.Repositories
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly XYZUniversityDbContext dbContext;

        public SQLStudentRepository(XYZUniversityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> DeleteAsync(Guid id)
        {
            var existingStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStudent == null)
            {
                return null;
            }
            dbContext.Students.Remove(existingStudent);
            await dbContext.SaveChangesAsync();
            return existingStudent;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await dbContext.Students
                           .Include(s => s.Payments) 
                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student?> UpdateAsync(Guid id, Student student)
        {
            var existingStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStudent == null)
            {
                return null;
            }
            existingStudent.Name = student.Name;
            existingStudent.CourseName = student.CourseName;
            existingStudent.EnrollmentStatus = student.EnrollmentStatus;
            await dbContext.SaveChangesAsync();
            return existingStudent;
        }
    }
}
