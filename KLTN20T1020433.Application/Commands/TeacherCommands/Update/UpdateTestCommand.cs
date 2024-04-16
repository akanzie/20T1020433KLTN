using MediatR;

namespace KLTN20T1020433.Web.Areas.Teacher.Commands.Update
{
    public class UpdateTestCommand : IRequest<bool>
    {
    }
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, bool>
    {
        public Task<bool> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
