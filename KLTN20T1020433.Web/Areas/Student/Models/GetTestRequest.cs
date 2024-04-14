using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Web.Areas.Student.Models
{
    public class GetTestRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public string SearchValue { get; set; } = "";
        public string StudentId { get; set; }
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;
    }
}
