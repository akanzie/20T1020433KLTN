

using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models.TestModel;

namespace KLTN20T1020433.Web.Models
{
    public class TestModel
    {
        public GetTestResponse Test { get; set; }
        public IEnumerable<GetTestFileResponse> Files { get; set; }
     
    }
}
