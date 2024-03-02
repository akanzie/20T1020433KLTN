using _20T1020433KLTN.Domain.Aggregates.StudentAggregate;
using _20T1020433KLTN.Infrastructure.Contexts;
using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyDbContext _context;

        public StudentRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetByUsername(string username)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Username == username);
        }

        // Implement other repository methods
    }
}
