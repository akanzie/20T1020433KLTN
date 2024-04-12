

using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Web.Models
{
    public class TestFileModel
    {
        public Test Test { get; set; }
        public List<TestFile>? Files { get; set; } = null;
    }
}
