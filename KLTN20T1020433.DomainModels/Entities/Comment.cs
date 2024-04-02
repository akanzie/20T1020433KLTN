using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DomainModelsModels.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public DateTime CommentedTime { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName{ get; set; }
        public int SubmissionId { get; set; }
    }
}
