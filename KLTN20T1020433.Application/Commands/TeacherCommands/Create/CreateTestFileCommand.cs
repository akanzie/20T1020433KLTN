using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateTestFileCommand : IRequest<bool>
    {
    }
    public class CreateTestFileCommandHandler : IRequestHandler<CreateTestFileCommand, bool>
    {
        public Task<bool> Handle(CreateTestFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
