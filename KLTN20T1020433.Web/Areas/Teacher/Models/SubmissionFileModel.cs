using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class SubmissionFileModel
    {
        public SubmissionStatus Status { get; set; }
        public IEnumerable<GetSubmissionFileResponse> Files { get; set; }
    }
}
