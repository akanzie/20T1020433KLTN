using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{
   
    public class GetSubmissionResponse
    {
        public int SubmissionId { get; set; }
        public DateTime SubmittedTime { get; set; }
        public SubmissionStatus Status { get; set; }
        public string StatusDisplayName { get; set; }
        public int TestId { get; set; }
        public string StudentName { get; set; }
    }
}
