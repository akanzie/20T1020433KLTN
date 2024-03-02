using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    public class Student : BaseEntity
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClass> CourseClasses { get; set; }

    }
}
