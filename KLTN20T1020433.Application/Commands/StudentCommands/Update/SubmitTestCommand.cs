using AutoMapper;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using System.Net;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Update
{
    public class SubmitTestCommand : IRequest<string>
    {
        public int SubmissionId { get; set; }
        public bool IsCheckIP { get; set; }
        public DateTime? TestEndTime { get; set; } = null;
        public string IPAddress { get; set; }
        public DateTime SubmittedTime { get; set; }
    }
    public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, string>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly ISubmissionFileRepository _submissionFileDB;
        public SubmitTestCommandHandler(ISubmissionRepository submissionDB, ISubmissionFileRepository submissionFileDB)
        {
            _submissionDB = submissionDB;
            _submissionFileDB = submissionFileDB;
        }
        public async Task<string> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
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

                if (request.SubmittedTime > request.TestEndTime)
                    submission.Status = SubmissionStatus.LateSubmission;
                else
                    submission.Status = SubmissionStatus.Submitted;
                if (request.IsCheckIP && await _submissionDB.CheckIPAddressExists(request.IPAddress, submission.TestId))
                {
                    submission.Status = SubmissionStatus.PendingProcessing;
                }
                submission.SubmittedTime = request.SubmittedTime;
                submission.IPAddress = request.IPAddress;
                await _submissionDB.Update(submission);
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
