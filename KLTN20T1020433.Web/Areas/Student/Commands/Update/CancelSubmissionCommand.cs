using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Commands.Update
{
    public class CancelSubmissionCommand : IRequest<bool>
    {
        public int SubmissionId { get; set; }
    }
}
