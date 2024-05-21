using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Delete
{
    public class RemoveSubmissionFileCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public DateTime? TestEndTime { get; set; } = null;
        public DateTime? TestStartTime { get; set; } = null;
        public bool CanSubmitLate { get; set; }
        public SubmissionStatus SubmissionStatus { get; set; }
    }
    public class RemoveSubmissionFileCommandHandler : IRequestHandler<RemoveSubmissionFileCommand, string>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;

        public RemoveSubmissionFileCommandHandler(ISubmissionFileRepository submissionFileDB)
        {
            _submissionFileDB = submissionFileDB;
        }
        public async Task<string> Handle(RemoveSubmissionFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.SubmissionStatus != SubmissionStatus.NotSubmitted)
                    return ErrorMessages.CannotRemoveFile;
                if (request.TestStartTime > DateTime.Now && request.TestStartTime != null)
                {
                    return ErrorMessages.CannotRemoveFile;
                }
                if (!request.CanSubmitLate && DateTime.Now > request.TestEndTime)
                    return ErrorMessages.CannotRemoveFile;
                var file = await _submissionFileDB.GetById(request.Id);
                if (file == null || !File.Exists(file.FilePath))
                {
                    return ErrorMessages.FileNotFound;
                }
                File.Delete(file.FilePath);
                await _submissionFileDB.Delete(request.Id);
                return $"Xóa tệp đính kèm {file.OriginalName} thành công.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }

    }
}
