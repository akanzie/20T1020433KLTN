using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class GetSubmissionFileResponse
    {
        public Guid FileId { get; set; }
        public string OriginalName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string MimeType { get; set; }
        public long Size { get; set; }
    }
    public class SubmissionFileModel
    {
        public SubmissionStatus Status { get; set; }
        public IEnumerable<GetSubmissionFileResponse> Files { get; set; }
    }
}
