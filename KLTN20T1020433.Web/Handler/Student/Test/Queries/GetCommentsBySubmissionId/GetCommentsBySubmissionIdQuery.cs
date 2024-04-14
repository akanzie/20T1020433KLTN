using KLTN20T1020433.Web.Areas.Student.Models.CommentModel;
using MediatR;

namespace KLTN20T1020433.Web.Handler.Student.Test.Queries.GetSubmission
{
    public class GetCommentsBySubmissionIdQuery : IRequest<IEnumerable<GetCommentResponse>>
    {        
        public int SubmissionId { get; set; }
    }
}
