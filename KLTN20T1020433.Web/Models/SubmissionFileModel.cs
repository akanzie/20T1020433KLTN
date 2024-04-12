

using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Models
{
    public class SubmissionFileModel
    {
        public Submission Submission { get; set; }
        public List<SubmissionFile>? Files { get; set; } = null;
    }
}
