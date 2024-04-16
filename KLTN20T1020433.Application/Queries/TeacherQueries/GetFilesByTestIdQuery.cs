using KLTN20T1020433.Application.DTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetFilesByTestIdQuery : IRequest<IEnumerable<GetTestFileResponse>>
    {
        public int TestId { get; set; }
    }
    public class GetTestFilesByTestIdQueryHandler : IRequestHandler<GetFilesByTestIdQuery, IEnumerable<GetTestFileResponse>>
    {
        public async Task<IEnumerable<GetTestFileResponse>> Handle(GetFilesByTestIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
