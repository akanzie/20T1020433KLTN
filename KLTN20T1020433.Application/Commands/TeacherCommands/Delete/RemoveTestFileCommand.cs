using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Delete
{
    public class RemoveTestFileCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
    public class RemoveTestFileCommandHandler : IRequestHandler<RemoveTestFileCommand, string>
    {
        private readonly ITestFileRepository _testFileDB;

        public RemoveTestFileCommandHandler(ITestFileRepository testFileDB)
        {
            _testFileDB = testFileDB;
        }
        public async Task<string> Handle(RemoveTestFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _testFileDB.GetById(request.Id);
                if (file == null || !File.Exists(file.FilePath))
                {
                    return ErrorMessages.FileNotFound;
                }
                File.Delete(file.FilePath);
                await _testFileDB.Delete(request.Id);
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
