using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;

namespace KLTN20T1020433.Web.Areas.Student.Models
{
    public class TestModel
    {
        public GetTestByIdResponse Test { get; set; }
        public IEnumerable<GetFileResponse> Files { get; set; }
    }
}
