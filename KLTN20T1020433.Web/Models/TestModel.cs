using KLTN20T102433.Domain.Entities;

namespace KLTN20T102433.Application.Models
{
    public class TestModel
    {
        public Test Test { get; set; }
        public List<TestFile> Files { get; set; }
    }
}
