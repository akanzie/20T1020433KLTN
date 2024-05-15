using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Areas.Student.Models
{
    public class SubmissionFileModel
    {
        public SubmissionStatus? Status { get; set; }
        public int TestId { get; set; }
        public IEnumerable<GetFileResponse> Files { get; set; }
    }
}
