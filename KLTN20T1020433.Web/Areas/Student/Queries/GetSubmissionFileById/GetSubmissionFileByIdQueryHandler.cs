using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionById;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionFileById
{
    public class GetSubmissionFileByIdQueryHandler : IRequestHandler<GetSubmissionFileByIdQuery, GetSubmissionFileResponse>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetSubmissionFileByIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionFileResponse> Handle(GetSubmissionFileByIdQuery request, CancellationToken cancellationToken)
        {
            var file = await _submissionFileDB.GetById(request.Id);
            if (file != null)
            {
                GetSubmissionFileResponse fileResponse = _mapper.Map<GetSubmissionFileResponse>(file);
                return fileResponse;
            }
            return new GetSubmissionFileResponse();
        }
    }
}
