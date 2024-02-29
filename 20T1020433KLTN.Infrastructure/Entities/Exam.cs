using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    public class Exam : BaseEntity
    {
        public string ExamName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int MaxScore { get; set; }

        public int CourseClassId { get; set; }
        public CourseClass CourseClass { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
