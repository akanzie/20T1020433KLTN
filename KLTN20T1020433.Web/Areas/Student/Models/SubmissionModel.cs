using KLTN20T1020433.Application.DTOs.StudentDTOs;

namespace KLTN20T1020433.Web.Areas.Student.Models
{
    public class SubmissionModel
    {
        public GetSubmissionResponse Submission { get; set; }
        public IEnumerable<GetCommentResponse> Comments { get; set; }      
 
    }
}
