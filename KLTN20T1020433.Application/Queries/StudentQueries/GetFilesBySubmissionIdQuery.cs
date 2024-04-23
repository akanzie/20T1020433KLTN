using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetFilesBySubmissionIdQuery : IRequest<IEnumerable<GetSubmissionFileResponse>>
    {
        public int SubmissionId { get; set; }
    }
    public class GetSubmissionFilesBySubmissionIdQueryHandler : IRequestHandler<GetFilesBySubmissionIdQuery, IEnumerable<GetSubmissionFileResponse>>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionFilesBySubmissionIdQueryHandler(ISubmissionFileRepository submissionFileDB, ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _submissionDB = submissionDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetSubmissionFileResponse>> Handle(GetFilesBySubmissionIdQuery request, CancellationToken cancellationToken)
        {

            var SubmissionFiles = await _submissionFileDB.GetFileBySubmissionId(request.SubmissionId);
            if (SubmissionFiles != null && SubmissionFiles.Any())
            {
                List<GetSubmissionFileResponse> SubmissionResponse = new List<GetSubmissionFileResponse>();
                foreach (var file in SubmissionFiles)
                {
                    GetSubmissionFileResponse getSubmissionFileResponse = _mapper.Map<GetSubmissionFileResponse>(file);
                    SubmissionResponse.Add(getSubmissionFileResponse);
                }
                return SubmissionResponse;
            }
            return new List<GetSubmissionFileResponse>();
        }
    }
}
