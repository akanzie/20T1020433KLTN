using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Queries.TeacherQueries;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class SubmissionModel
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public GetSubmissionResponse Submission { get; set; }
        public IEnumerable<GetFileResponse> Files { get; set; }
        public GetSubmissionsBySearchQuery SearchQuery { get; set; }
    }
}
