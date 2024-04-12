using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;

namespace KLTN20T1020433.Web.Models
{
    public class TestSearchResult : BasePaginationResult
    {
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;

        public List<Test> Data { get; set; } = new List<Test>();
    }
}
