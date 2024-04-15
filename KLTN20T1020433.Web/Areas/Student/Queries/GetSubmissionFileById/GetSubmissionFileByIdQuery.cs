using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionFileById
{
    public class GetSubmissionFileByIdQuery : IRequest<GetSubmissionFileResponse>
    {
        public Guid Id { get; set; }
    }
}
