using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using KLTN20T1020433.Application.DTOs.StudentDTOs;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetSubmissionByStudentIdAndTestIdQuery : IRequest<GetSubmissionResponse>
    {
        public string StudentId { get; set; }
        public int TestId { get; set; }
    }
    public class GetSubmissionByStudentIdAndTestIdQueryHandler : IRequestHandler<GetSubmissionByStudentIdAndTestIdQuery, GetSubmissionResponse>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionByStudentIdAndTestIdQueryHandler(ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionResponse> Handle(GetSubmissionByStudentIdAndTestIdQuery request, CancellationToken cancellationToken)
        {
            var submission = await _submissionDB.GetByTestIdAndStudentId(request.TestId, request.StudentId);
            if (submission != null)
            {
                GetSubmissionResponse testResponse = _mapper.Map<GetSubmissionResponse>(submission);
                return testResponse;
            }
            return new GetSubmissionResponse();
        }
    }
}
