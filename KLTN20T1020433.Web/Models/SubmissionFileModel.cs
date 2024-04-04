using KLTN20T1020433.DomainModels.Entities;

namespace KLTN20T1020433.Web.Models
{
    public class SubmissionFileModel
    {
        public Submission Submission { get; set; }
        public List<SubmissionFile>? SubmissionFiles { get; set; } = null;
    }
}
