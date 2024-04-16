using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetSubmissionsByTestIdQuery : IRequest<IEnumerable<GetSubmissionResponse>>
    {
        public int TestId { get; set; }
    }
    public class GetSubmissionsByTestIdQueryHandler : IRequestHandler<GetSubmissionsByTestIdQuery, IEnumerable<GetSubmissionResponse>>
    {
        public Task<IEnumerable<GetSubmissionResponse>> Handle(GetSubmissionsByTestIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
