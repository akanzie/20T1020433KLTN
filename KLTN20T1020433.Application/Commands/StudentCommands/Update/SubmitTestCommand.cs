using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Update
{
    public class SubmitTestCommand : IRequest<string>
    {
        public int SubmissionId { get; set; }
        public bool IsCheckIP { get; set; }
        public bool CanSubmitLate { get; set; }
        public DateTime? TestStartTime { get; set; } = null;
        public DateTime? TestEndTime { get; set; } = null;
        public string IPAddress { get; set; }
        public DateTime SubmitTime { get; set; }
    }
    public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, string>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly ISubmissionHistoryRepository _submissionHistoryDB;
        private readonly ISubmissionFileRepository _submissionFileDB;
        public SubmitTestCommandHandler(ISubmissionRepository submissionDB, ISubmissionFileRepository submissionFileDB, ISubmissionHistoryRepository submissionHistoryDB)
        {
            _submissionDB = submissionDB;
            _submissionFileDB = submissionFileDB;
            _submissionHistoryDB = submissionHistoryDB;
        }
        public async Task<string> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.TestStartTime > DateTime.Now && request.TestStartTime != null)
                {
                    return ErrorMessages.CannotSubmit;
                }
                if (!request.CanSubmitLate && DateTime.Now > request.TestEndTime)
                {
                    return ErrorMessages.CannotSubmit;
                }
                var submission = await _submissionDB.GetById(request.SubmissionId);
                if (submission == null)
                {
                    return ErrorMessages.SubmissionNotFound;
                }
                var files = await _submissionFileDB.GetFilesBySubmissionId(request.SubmissionId);
                if (files == null || !files.Any())
                {
                    return ErrorMessages.CannotSubmitWithoutUpload;
                }

                if (request.SubmitTime > request.TestEndTime)
                    submission.Status = SubmissionStatus.LateSubmission;
                else
                    submission.Status = SubmissionStatus.Submitted;
                if (request.IsCheckIP && await _submissionHistoryDB.CheckIPAddressExists(request.IPAddress, request.SubmissionId, submission.TestId))
                {
                    submission.Status = SubmissionStatus.PendingProcessing;
                }
                submission.SubmitTime = request.SubmitTime;
                await _submissionDB.Update(submission);
                await _submissionHistoryDB.Add(new SubmissionHistory { SubmissionId = request.SubmissionId, IPAddress = request.IPAddress, SubmitTime = request.SubmitTime });
                return SuccessMessages.SubmitSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
