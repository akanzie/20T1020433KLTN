using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Application.DTOs.StudentDTOs
{
    public class GetSubmissionFileResponse
    {
        public Guid? FileId { get; set; } = null;
        public string OriginalName { get; set; }
        public string FilePath { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
    }

}
