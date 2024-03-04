using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Domain.Entities;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    public class StudentDto : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClassDto> CourseClasses { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
