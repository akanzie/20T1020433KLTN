using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionFilesBySubmissionId
{
    public class GetSubmissionFilesBySubmissionIdQuery : IRequest<IEnumerable<GetSubmissionFileResponse>>
    {
        public int SubmissionId { get; set; }
    }
}
