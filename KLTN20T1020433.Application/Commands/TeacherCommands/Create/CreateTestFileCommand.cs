using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateTestFileCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }
        public int TestId { get; set; }
    }
    public class CreateTestFileCommandHandler : IRequestHandler<CreateTestFileCommand, bool>
    {
        private readonly ITestRepository _testDB;
        private readonly FileConfig _fileOptions;
        private readonly ITestFileRepository _testFileDB;
        public CreateTestFileCommandHandler(ITestRepository testDB, ITestFileRepository testFileDB, IOptions<FileConfig> options)
        {
            _testFileDB = testFileDB;
            _testDB = testDB;
            _fileOptions = options.Value;
        }
        public async Task<bool> Handle(CreateTestFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.File == null || request.File.Length == 0)
                {
                    throw new ArgumentException("Invalid file.");
                }
                Guid id = Guid.NewGuid();
                string uniqueFileName = $"{id}_{request.File.FileName}";

                Test test = await _testDB.GetById(request.TestId);
                string directoryPath = Path.Combine(_fileOptions.FileStoragePath, test.Title, "Test");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = Path.Combine(directoryPath, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }
                TestFile file = new TestFile
                {
                    FileId = id,
                    FileName = uniqueFileName,
                    FilePath = filePath,
                    MimeType = request.File.ContentType,
                    Size = request.File.Length,
                    TestId = request.TestId,
                    OriginalName = request.File.FileName
                };
                var result = await _testFileDB.Add(file);
                return result;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log và thông báo cho người dùng
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu tạo tệp đính kèm cho kỳ thi: " + ex.Message);
                throw;
            }
        }
    }
}
