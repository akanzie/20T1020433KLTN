using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Entities.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Entities
{
    public class CourseClassDto {  
        public string CourseName { get; set; }

        public Guid LecturerId { get; set; }
        public LecturerDto Lecturer { get; set; }

        public ICollection<StudentDto> Students { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
