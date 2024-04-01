using KLTN20T1020433.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DomainModels.Entities.Lecturer
{
    public class LecturerDto
    {
        private String FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClassDto> CourseClasses { get; set; }

    }
}
