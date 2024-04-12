using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Models
{
    public class SubmissionModel
    {
        public Submission Submission { get; set; }
        public List<Comment>? Comments { get; set; } = null;
    }
}
