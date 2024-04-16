using KLTN20T1020433.Application.DTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetTestFileByIdQuery : IRequest<GetTestFileResponse>
    {
        public Guid Id { get; set; }
    }
    public class GetTestFileByIdQueryHandler : IRequestHandler<GetTestFileByIdQuery, GetTestFileResponse>
    {
        public Task<GetTestFileResponse> Handle(GetTestFileByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
