using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Areas.Student.Models.CommentModel;

namespace KLTN20T1020433.Web.Models
{
    public class SubmissionModel
    {
        public GetSubmissionResponse Submission { get; set; }
        public IEnumerable<GetCommentResponse> Comments { get; set; }
    }
}
