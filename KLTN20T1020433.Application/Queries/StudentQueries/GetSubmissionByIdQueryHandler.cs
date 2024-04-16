using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetSubmissionByIdQuery : IRequest<GetSubmissionResponse>
    {
        public int Id { get; set; }
    }
    public class GetSubmissionByIdQueryHandler : IRequestHandler<GetSubmissionByIdQuery, GetSubmissionResponse>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionByIdQueryHandler(ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionResponse> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
        {
            var submission = await _submissionDB.GetById(request.Id);
            if (submission != null)
            {
                GetSubmissionResponse testResponse = _mapper.Map<GetSubmissionResponse>(submission);
                return testResponse;
            }
            return new GetSubmissionResponse();
        }
    }
}
