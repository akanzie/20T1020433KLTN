using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionFilesBySubmissionId
{
    public class GetSubmissionFilesBySubmissionIdQueryHandler : IRequestHandler<GetSubmissionFilesBySubmissionIdQuery, IEnumerable<GetSubmissionFileResponse>>
    {
        private readonly ISubmissionFileRepository _SubmissionFileDB;
        private readonly IMapper _mapper;

        public GetSubmissionFilesBySubmissionIdQueryHandler(ISubmissionFileRepository SubmissionFileDB, IMapper mapper)
        {
            _SubmissionFileDB = SubmissionFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetSubmissionFileResponse>> Handle(GetSubmissionFilesBySubmissionIdQuery request, CancellationToken cancellationToken)
        {
            var SubmissionFiles = await _SubmissionFileDB.GetFileBySubmissionId(request.SubmissionId);
            if (SubmissionFiles != null && !SubmissionFiles.Any())
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
