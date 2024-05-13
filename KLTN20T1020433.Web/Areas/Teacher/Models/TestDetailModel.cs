using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.DTOs;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class TestDetailModel
    {
        public GetTestDetailResponse Test { get; set; }
        public IEnumerable<GetTestFileResponse> Files { get; set; }
    }
}
