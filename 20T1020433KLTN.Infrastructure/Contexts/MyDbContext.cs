using _20T1020433KLTN.Domain.Aggregates.Exam;
using _20T1020433KLTN.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Contexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        // Các DbSet khác có thể được thêm vào tùy theo yêu cầu của ứng dụng

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình các quan hệ, chỉ mục, ràng buộc và các thuộc tính khác của các đối tượng entity
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Gọi hàm base để hoàn thiện cấu hình
            base.OnModelCreating(modelBuilder);
        }
    }
}
