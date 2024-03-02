using _20T1020433KLTN.Domain.Aggregates.Exam;
using _20T1020433KLTN.Domain.Entities.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    public class CourseClass : BaseEntity
    {
        public string CourseName { get; set; }

        public Guid LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
