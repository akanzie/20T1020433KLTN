using _20T1020433KLTN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    [Table("CourseClasses")]
    public class CourseClass : BaseEntity
    {
        public string CourseName { get; set; }

        public string LecturerId { get; set; }
        [ForeignKey("LecturerId")]
        public Lecturer Lecturer { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
