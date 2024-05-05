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

            var submission = await _submissionDB.GetById(request.SubmissionId);
            if (submission == null)
            {
                return "Không tìm thấy bài nộp.";
            }
            var files = await _submissionFileDB.GetFilesBySubmissionId(request.SubmissionId);
            if (files == null || !files.Any())
            {
                return "Bạn không thể nộp bài khi chưa tải file bài nộp lên.";
            }

            if (request.SubmittedTime > request.TestEndTime)
                submission.Status = SubmissionStatus.LateSubmission;
            else
                submission.Status = SubmissionStatus.Submitted;
            if (request.IsCheckIP && Utils.CheckIPAddressExists(request.IPAddress))
            {
                submission.Status = SubmissionStatus.PendingProcessing;
            }
            submission.SubmittedTime = request.SubmittedTime;
            submission.IPAddress = request.IPAddress;
            if (await _submissionDB.Update(submission))
            {
                return "Nộp bài thành công.";
            }
            return "Có lỗi xảy ra, vui lòng thử lại sau.";
        }
    }
}
