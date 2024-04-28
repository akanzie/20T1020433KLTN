using AutoMapper;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using System.Net;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Update
{
    public class SubmitTestCommand : IRequest<bool>
    {
        public int SubmissionId { get; set; }
        public bool IsCheckIP { get; set; }
        public DateTime? TestEndTime { get; set; } = null;
        public string IPAddress { get; set; }
        public DateTime SubmittedTime { get; set; }
    }
    public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, bool>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly ISubmissionFileRepository _submissionFileDB;
        public SubmitTestCommandHandler(ISubmissionRepository submissionDB, ISubmissionFileRepository submissionFileDB)
        {
            _submissionDB = submissionDB;
            _submissionFileDB = submissionFileDB;
        }
        public async Task<bool> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool result = false;
                
                var files = await _submissionFileDB.GetFilesBySubmissionId(request.SubmissionId);
                if (files == null || !files.Any())
                {
                    return result;
                }    
                Submission? submission = await _submissionDB.GetById(request.SubmissionId);            
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
