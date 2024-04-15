using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetSubmission
{
    public class GetSubmissionByStudentIdAndTestIdQuery : IRequest<GetSubmissionResponse>
    {
        public string StudentId { get; set; }
        public int TestId { get; set; }
    }
}
