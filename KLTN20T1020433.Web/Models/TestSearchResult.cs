using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models;

namespace KLTN20T1020433.Web.Models
{
    public class TestSearchResult : BasePaginationResult
    {
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;

        public IEnumerable<GetTestBySearchResponse> Data { get; set; }
    }
}
