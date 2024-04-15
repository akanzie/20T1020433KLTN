using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Commands.Create
{
    public class CreateSubmissionFileCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }
        public int SubmissionId { get; set; }
    }
}
