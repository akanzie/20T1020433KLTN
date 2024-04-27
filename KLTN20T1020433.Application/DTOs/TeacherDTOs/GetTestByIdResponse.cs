using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{

    public class GetTestByIdResponse
    {
        public int TestId { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Instruction { get; set; } = string.Empty;
        public TestType? TestType { get; set; } = null;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public DateTime? LastUpdateTime { get; set; } = null;
        public bool IsCheckIP { get; set; }
        public bool IsConductedAtSchool { get; set; }
        public bool CanSubmitLate { get; set; }
        public TestStatus? Status { get; set; } = null;
    }

}
