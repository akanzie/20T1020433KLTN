using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Commands.Delete
{
    public class RemoveSubmissionFileCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
