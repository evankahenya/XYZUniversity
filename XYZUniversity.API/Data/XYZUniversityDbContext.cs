using Microsoft.EntityFrameworkCore;
using XYZUniversity.API.Models.Domain;

namespace XYZUniversity.API.Data 
{
    public class XYZUniversityDbContext : DbContext
    {
        public XYZUniversityDbContext(DbContextOptions<XYZUniversityDbContext> dbContextOptions) : base(dbContextOptions) { }

        // DbSet for Students
        public DbSet<Student> Students { get; set; }

        // DbSet for Payments
        public DbSet<Payment> Payments { get; set; }
    }
}
