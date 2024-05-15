using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class SubmissionModel
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public GetSubmissionResponse Submission { get; set; }
        public IEnumerable<GetFileResponse> Files { get; set; }
        public IEnumerable<GetSubmissionBySearchResponse> Submissions { get; set; }        
    }
}
