using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetSubmissionFileByIdQuery : IRequest<GetSubmissionFileResponse>
    {
        public Guid Id { get; set; }
        public int TestId { get; set; }
        public string StudentId { get; set; }
    }
    public class GetSubmissionFileByIdQueryHandler : IRequestHandler<GetSubmissionFileByIdQuery, GetSubmissionFileResponse>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionFileByIdQueryHandler(ISubmissionFileRepository submissionFileDB, ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _submissionDB = submissionDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionFileResponse> Handle(GetSubmissionFileByIdQuery request, CancellationToken cancellationToken)
        {
            Submission? submission = await _submissionDB.GetByTestIdAndStudentId(request.TestId, request.StudentId);
            if (submission != null)
            {
                var file = await _submissionFileDB.GetById(request.Id);
                if (file != null)
                {
                    GetSubmissionFileResponse fileResponse = _mapper.Map<GetSubmissionFileResponse>(file);
                    return fileResponse;
                }
            }
            return new GetSubmissionFileResponse();
        }
    }
}
