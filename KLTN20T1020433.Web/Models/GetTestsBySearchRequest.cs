using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Web.Models
{
    public class GetTestsBySearchRequest : PaginationSearchInput
    {
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;
    }
}
