using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.DTOs.StudentDTOs
{
    public class GetTestBySearchResponse
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string StartTime { get; set; } = "";
        public string EndTime { get; set; } = "";
        public TestStatus Status { get; set; }
        public string StatusDisplayName { get; set; }   
        public string TeacherName { get; set; }
    }
}
