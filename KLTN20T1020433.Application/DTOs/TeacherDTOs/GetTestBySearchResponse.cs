using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{
    public class GetTestBySearchResponse
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string? CreatedTime { get; set; }
        public string? LastUpdateTime { get; set; }
        public string? EndTime { get; set; }
        public string StatusDisplayName { get; set; }
        public string TeacherName { get; set; }
    }
}
