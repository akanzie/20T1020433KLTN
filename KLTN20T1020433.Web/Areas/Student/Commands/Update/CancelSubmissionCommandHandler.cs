using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionFilesBySubmissionId;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Commands.Update
{
    public class CancelSubmissionCommandHandler : IRequestHandler<CancelSubmissionCommand, bool>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CancelSubmissionCommandHandler(ISubmissionRepository submissionDB, ITestRepository testDB, ISubmissionFileRepository submissionFileDB, IMapper mapper, IMediator mediator)
        {
            _submissionDB = submissionDB;
            _testDB = testDB;
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<bool> Handle(CancelSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool result = false;
                Submission submission = await _submissionDB.GetById(request.SubmissionId);
                submission.Status = SubmissionStatus.NotSubmitted;
                result = await _submissionDB.Update(submission);
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
