using MdtrmExam.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MdtrmExam.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Section> Sections { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>().HasKey(s => s.StudentId);
            builder.Entity<Course>().HasKey(c => c.CourseId);

            builder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);
                entity.Property(s => s.StudentNo).IsRequired().HasMaxLength(20);
                entity.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(s => s.LastName).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Email).IsRequired().HasMaxLength(100);
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);
                entity.Property(c => c.Code).IsRequired().HasMaxLength(10);
                entity.Property(c => c.Title).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Units).IsRequired();
            });

            builder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.DepartmentId);
                entity.Property(d => d.Code).IsRequired().HasMaxLength(20);
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            });

            builder.Entity<Instructor>(entity =>
            {
                entity.HasKey(i => i.InstructorId);
                entity.Property(i => i.FirstName).IsRequired().HasMaxLength(30);
                entity.Property(i => i.LastName).IsRequired().HasMaxLength(30);
                entity.Property(i => i.Email).IsRequired().HasMaxLength(100);
                entity.HasOne<Department>().WithMany().HasForeignKey(i => i.DepartmentId);
            });

            builder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId);
                entity.Property(e => e.Semester).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Grade).HasColumnType("decimal(5, 2)");
                entity.HasOne<Student>().WithMany().HasForeignKey(e => e.StudentId);
                entity.HasOne<Course>().WithMany().HasForeignKey(e => e.CourseId);
            });


            builder.Entity<Section>(entity =>
            {
                entity.HasKey(s => s.SectionId);
                entity.Property(s => s.SectionCode).IsRequired().HasMaxLength(20);
                entity.Property(s => s.Room).IsRequired().HasMaxLength(30);
                entity.Property(s => s.Schedule).IsRequired().HasMaxLength(50);
                entity.HasOne<Course>().WithMany().HasForeignKey(s => s.CourseId);
                entity.HasOne<Instructor>().WithMany().HasForeignKey(s => s.InstructorId);
            });
        }
    }
}