using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Update
{
    public class CancelSubmissionCommand : IRequest<string>
    {
        public int SubmissionId { get; set; }
        public bool CanSubmitLate { get; set; }
        public DateTime? TestEndTime { get; set; } = null;
    }
    public class CancelSubmissionCommandHandler : IRequestHandler<CancelSubmissionCommand, string>
    {
        private readonly ISubmissionRepository _submissionDB;
        public CancelSubmissionCommandHandler(ISubmissionRepository submissionDB)
        {
            _submissionDB = submissionDB;
        }
        public async Task<string> Handle(CancelSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.CanSubmitLate && DateTime.Now > request.TestEndTime)
                {
                    return ErrorMessages.CannotCancelSubmit;
                }
                var submission = await _submissionDB.GetById(request.SubmissionId);
                if (submission == null)
                {
                    return ErrorMessages.SubmissionNotFound;
                }
                submission.Status = SubmissionStatus.NotSubmitted;
                await _submissionDB.Update(submission);
                return SuccessMessages.CancelSubmissionSuccess;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
