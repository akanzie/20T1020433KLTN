using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN20T102433.Domain.Entities;

namespace KLTN20T102433.Infrastructure.Entities
{
    public class StudentDto 
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClassDto> CourseClasses { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
