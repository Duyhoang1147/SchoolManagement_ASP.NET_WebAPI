using Microsoft.EntityFrameworkCore;
using SchoolManagerment_WebAPI.Model;

namespace SchoolManagerment_WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherClassroom> TeacherClassrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTpcMappingStrategy();

            modelBuilder.Entity<Teacher>().ToTable("Teachers");

            modelBuilder.Entity<Student>().ToTable("Students");

            modelBuilder.Entity<TeacherClassroom>()
                .HasKey(tc => new { tc.TeacherId, tc.ClassroomId });

            modelBuilder.Entity<TeacherClassroom>()
                .HasOne(tc => tc.Teacher)
                .WithMany(t => t.TeacherClassrooms)
                .HasForeignKey(tc => tc.TeacherId);

            modelBuilder.Entity<TeacherClassroom>()
                .HasOne(tc => tc.Classroom)
                .WithMany(c => c.TeacherClassrooms)
                .HasForeignKey(tc => tc.ClassroomId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Student_Classroom)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.Student_ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}