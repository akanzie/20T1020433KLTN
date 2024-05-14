using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.Queries.TeacherQueries;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class TestDetailModel
    {
        public GetTestDetailResponse Test { get; set; }
        public IEnumerable<GetTestFileResponse> Files { get; set; }
        public GetSubmissionsBySearchQuery SearchQuery { get; set; }
    }
}
