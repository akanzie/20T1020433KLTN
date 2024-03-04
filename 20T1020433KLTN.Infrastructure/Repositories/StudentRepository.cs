using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Interfaces;
using _20T1020433KLTN.Infrastructure.Contexts;
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

    

        // Implement other repository methods
    }
}
