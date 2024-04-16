using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{

    public class GetTestByIdResponse
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public TestType TestType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? LastUpdateTime { get; set; } = null;
        public bool IsCheckIP { get; set; }
        public bool IsConductedAtSchool { get; set; }
        public TestStatus Status { get; set; }
    }

}
