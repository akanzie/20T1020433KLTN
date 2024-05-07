using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Domain.Test;


namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class TestFileModel
    {
        public IEnumerable<GetTestFileResponse> Files { get; set; }
    }
}
