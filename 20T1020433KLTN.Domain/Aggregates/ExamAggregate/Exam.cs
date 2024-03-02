using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Domain.Entities.Lecturer;
using _20T1020433KLTN.Infrastructure.Entities;

namespace _20T1020433KLTN.Domain.Aggregates.Exam
{
    public class Exam : BaseEntity
    {   private string ExamName { get; set; }
        public Guid CourseClassId { get; set; }
        public CourseClass CourseClass { get; set; }
        private ICollection<Submission> Submissions { get; set; }
    }
}
