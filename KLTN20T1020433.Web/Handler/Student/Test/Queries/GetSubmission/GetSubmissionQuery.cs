using KLTN20T1020433.Web.Areas.Student.Models.SubmissionModel;
using MediatR;

namespace KLTN20T1020433.Web.Handler.Student.Test.Queries.GetSubmission
{
    public class GetSubmissionQuery : IRequest<GetSubmissionResponse>
    {
        public string StudentId { get; set; }
        public int TestId { get; set; }
    }
}
