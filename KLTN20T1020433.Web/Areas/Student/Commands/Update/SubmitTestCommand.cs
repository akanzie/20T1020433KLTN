using MediatR;
using System.Net;

namespace KLTN20T1020433.Web.Areas.Student.Commands.Update
{
    public class SubmitTestCommand : IRequest<bool>
    {
        public int SubmissionId { get; set; }
        public bool IsCheckIP { get; set; }
        public DateTime? TestEndTime { get; set; } = null;
        public IPAddress IPAddress { get; set; }
        public DateTime SubmittedTime { get; set; }
    }
}
