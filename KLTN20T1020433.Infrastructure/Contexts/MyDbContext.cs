using KLTN20T102433.Domain.Aggregates.Exam;
using KLTN20T102433.Domain.Entities;
using KLTN20T102433.Infrastructure.Configurations;
using KLTN20T102433.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.Infrastructure.Contexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> CourseClasses { get; set; }
        public DbSet<Test> Exams { get; set; }
        public DbSet<Teacher> Lecturers { get; set; }
        public DbSet<Submission> Submissions { get; set; }


        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ExamConfiguration());
            modelBuilder.ApplyConfiguration(new CourseClassConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new SubmissionConfiguration());
            modelBuilder.ApplyConfiguration(new LecturerConfiguration());

            //modelBuilder.Entity<Role>()
            //            .HasMany<User>(r => r.Users)
            //            .WithOne(u => u.Role)
            //            .HasForeignKey(u => u.RoleId)
            //            .OnDelete(DeleteBehavior.Cascade);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
