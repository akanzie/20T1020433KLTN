using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class SubmissionModel
    {
        public GetSubmissionResponse Submission { get; set; }
        public IEnumerable<GetFileResponse> Files { get; set; }
    }
}
