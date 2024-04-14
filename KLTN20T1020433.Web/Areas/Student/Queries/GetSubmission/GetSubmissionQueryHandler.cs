using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Handler.Student.Test.Queries.GetTest;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetSubmission
{
    public class GetSubmissionQueryHandler : IRequestHandler<GetSubmissionQuery, GetSubmissionResponse>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionQueryHandler(ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionResponse> Handle(GetSubmissionQuery request, CancellationToken cancellationToken)
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
