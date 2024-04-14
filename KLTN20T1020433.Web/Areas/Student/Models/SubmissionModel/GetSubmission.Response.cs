using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Areas.Student.Models.SubmissionModel
{
    public class GetSubmissionResponse
    {
        public int SubmissionId { get; set; }
        public DateTime SubmittedTime { get; set; }                 
        public string StatusDescription { get; set; }
        
    }
}
