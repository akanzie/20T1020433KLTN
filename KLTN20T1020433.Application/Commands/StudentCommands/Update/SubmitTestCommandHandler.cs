using AutoMapper;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;
using System.Net;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Update
{
    public class SubmitTestCommand : IRequest<bool>
    {
        public int SubmissionId { get; set; }
        public bool IsCheckIP { get; set; }
        public DateTime? TestEndTime { get; set; } = null;
        public IPAddress IPAddress { get; set; }
        public DateTime SubmittedTime { get; set; }
    }
    public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, bool>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public SubmitTestCommandHandler(ISubmissionRepository submissionDB, ITestRepository testDB, ISubmissionFileRepository submissionFileDB, IMapper mapper, IMediator mediator)
        {
            _submissionDB = submissionDB;
            _testDB = testDB;
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<bool> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool result = false;
                var files = await _mediator.Send(new GetSubmissionFilesBySubmissionIdQuery { SubmissionId = request.SubmissionId });
                if (files == null || !files.Any())
                {
                    return result;
                }
                Submission submission = await _submissionDB.GetById(request.SubmissionId);
                if (request.SubmittedTime > request.TestEndTime)
                    submission.Status = SubmissionStatus.LateSubmission;
                else
                    submission.Status = SubmissionStatus.Submitted;
                if (request.IsCheckIP && Utils.CheckIPAddressExists(request.IPAddress))
                {
                    submission.Status = SubmissionStatus.PendingProcessing;
                }
                submission.SubmittedTime = request.SubmittedTime;
                submission.IPAddress = request.IPAddress.ToString();   
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
