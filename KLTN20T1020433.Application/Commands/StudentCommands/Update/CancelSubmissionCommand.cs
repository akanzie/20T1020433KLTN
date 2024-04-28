using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Update
{
    public class CancelSubmissionCommand : IRequest<bool>
    {
        public int SubmissionId { get; set; }
    }
    public class CancelSubmissionCommandHandler : IRequestHandler<CancelSubmissionCommand, bool>
    {
        private readonly ISubmissionRepository _submissionDB; 
        public CancelSubmissionCommandHandler(ISubmissionRepository submissionDB)
        {
            _submissionDB = submissionDB;

        }
        public async Task<bool> Handle(CancelSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool result = false;
                Submission? submission = await _submissionDB.GetById(request.SubmissionId);
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
