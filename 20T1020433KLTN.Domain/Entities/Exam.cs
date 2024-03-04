using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Infrastructure.Entities;

namespace _20T1020433KLTN.Domain.Entities
{
    [Table("Exams")]
    public class Exam : BaseEntity
    {
        private string ExamName { get; set; }
        private string Body { get; set; }
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }
        public Guid CourseClassId { get; set; }
        [ForeignKey("CourseClassId")]
        public CourseClass CourseClass { get; set; }
        private ICollection<Submission> Submissions { get; set; }
        private string LecturerId { get; set; }
        [ForeignKey("LecturerId")]
        private Lecturer Lecturer { get; set; }
    }
}
