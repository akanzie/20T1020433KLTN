using KLTN20T1020433.DomainModels.Entities; 

namespace KLTN20T1020433.Web.Models
{
    public class SubmissionModel
    {
        public Submission Submission { get; set; }
        public List<Comment>? Comments { get; set; } = null;
    }
}
