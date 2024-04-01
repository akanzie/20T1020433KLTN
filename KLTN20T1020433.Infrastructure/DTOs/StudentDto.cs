using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN20T1020433.DomainModels.Entities;

namespace KLTN20T1020433.Infrastructure.Entities
{
    public class StudentDto 
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClassDto> CourseClasses { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
