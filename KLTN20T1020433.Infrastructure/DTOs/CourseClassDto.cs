using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Entities.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    public class CourseClassDto {  
        public string CourseName { get; set; }

        public Guid LecturerId { get; set; }
        public LecturerDto Lecturer { get; set; }

        public ICollection<StudentDto> Students { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
