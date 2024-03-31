using KLTN20T102433.Domain.Entities;
using KLTN20T102433.Domain.Enum;

namespace KLTN20T102433.Application.Models
{
    public class TestSearchResult : BasePaginationResult
    {
        public TestType Type { get; set; } = TestType.All;
        public TestStatus Status { get; set; } = TestStatus.All;
        public string TimeRange { get; set; } = "";
        public List<Test> Data { get; set; } = new List<Test>();
    }
}
