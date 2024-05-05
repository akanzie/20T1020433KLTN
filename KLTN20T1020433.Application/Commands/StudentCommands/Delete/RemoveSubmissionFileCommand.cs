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
            var file = await _submissionFileDB.GetById(request.Id);
            if (file == null || !File.Exists(file.FilePath))
            {
                return "Không tìm thấy file.";
            }
            File.Delete(file.FilePath);
            if (await _submissionFileDB.Delete(request.Id))
            {
                return $"Xóa file {file.OriginalName} thành công";
            }
            return "Có lỗi xảy ra, vui lòng thử lại sau.";
        }
    }
}
