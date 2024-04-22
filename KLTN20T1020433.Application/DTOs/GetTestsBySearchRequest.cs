using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.DTOs
{
    public class GetTestsBySearchRequest : PaginationSearchInput
    {
        public int Semester { get; set; } = 0;
        public string AcademicYear { get; set; } = "";
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;
    }
}
