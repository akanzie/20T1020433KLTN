using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateTestCommand : IRequest<int>
    {
    }
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, int>
    {
        public Task<int> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
