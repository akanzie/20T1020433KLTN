using KLTN20T102433.Domain.Entities;
using KLTN20T102433.Domain.Entities.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.Infrastructure.Entities
{
    public class CourseClassDto {  
        public string CourseName { get; set; }

        public Guid LecturerId { get; set; }
        public LecturerDto Lecturer { get; set; }

        public ICollection<StudentDto> Students { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
