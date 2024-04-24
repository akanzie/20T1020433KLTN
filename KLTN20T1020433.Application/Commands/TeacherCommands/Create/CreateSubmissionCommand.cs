using KLTN20T1020433.Domain.Submission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateSubmissionCommand : IRequest<int>
    {
        public string StudentId { get; set; }
        public int TestId { get; set; }
        public SubmissionStatus Status { get; set; } = SubmissionStatus.NotSubmitted;
    }
    public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, int>
    {
        public Task<int> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
