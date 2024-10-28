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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    //seed the data for payments

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the one-to-many relationship between Student and Payment
            modelBuilder.Entity<Payment>()
                .HasOne<Student>()
                .WithMany(student => student.Payments)
                .HasForeignKey(payment => payment.StudentId);
        }



    }
    
}
