using KLTN20T1020433.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class StudentRepository 
    {
        private readonly MyDbContext _context;

        public StudentRepository(MyDbContext context)
        {
            _context = context;
        }

    

        // Implement other repository methods
    }
}
