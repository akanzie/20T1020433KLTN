using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Entities.Lecturer
{
    public class Lecturer : BaseEntity
    {
        private String FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClass> CourseClasses { get; set; }

    }
}
