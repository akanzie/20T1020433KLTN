using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class TestModel
    {
        public GetTestByIdResponse Test { get; set; }
        public IEnumerable<GetTestFileResponse> Files { get; set; }
    }
}
