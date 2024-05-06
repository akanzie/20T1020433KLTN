using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Delete
{
    public class RemoveSubmissionFileCommand : IRequest<string>
    {
        public Guid Id { get; set; }
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
