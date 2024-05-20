using AutoMapper;
using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Create
{
    public class CreateSubmissionFileCommand : IRequest<bool>
    {
        public int SubmissionId { get; set; }
        public IFormFile File { get; set; }
        public DateTime? TestStartTime { get; set; } = null;
        public DateTime? TestEndTime { get; set; } = null;
        public string TestTitle { get; set; } = "";
        public bool CanSubmitLate { get; set; }
    }
    public class CreateSubmissionFileCommandHandler : IRequestHandler<CreateSubmissionFileCommand, bool>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly FileConfig _fileOptions;
        public CreateSubmissionFileCommandHandler(ISubmissionFileRepository submissionFileDB, ISubmissionRepository submissionDB, IOptions<FileConfig> options)
        {
            _submissionFileDB = submissionFileDB;
            _submissionDB = submissionDB;
            _fileOptions = options.Value;
        }
        public async Task<bool> Handle(CreateSubmissionFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.TestStartTime > DateTime.Now && request.TestStartTime != null)
                {
                    throw new ArgumentException("Cannot Create.");
                }
                if (!request.CanSubmitLate && DateTime.Now > request.TestEndTime)
                    throw new ArgumentException("Cannot Create.");               
                if (request.File == null || request.File.Length == 0 || request.File.Length >= FileUtils.MAX_FILE_SIZE)
                {
                    throw new ArgumentException("Invalid file.");
                }

                Guid id = Guid.NewGuid();
                string uniqueFileName = $"{id}_{request.File.FileName}";
                Submission? submission = await _submissionDB.GetById(request.SubmissionId);
                string directoryPath = Path.Combine(_fileOptions.FileStoragePath, request.TestTitle, "Submission");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = Path.Combine(directoryPath, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }
                SubmissionFile file = new SubmissionFile
                {
                    FileId = id,
                    FileName = uniqueFileName,
                    FilePath = filePath,
                    MimeType = request.File.ContentType,
                    Size = request.File.Length,
                    SubmissionId = request.SubmissionId,
                    OriginalName = request.File.FileName
                };
                var result = await _submissionFileDB.Add(file);
                return result;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log và thông báo cho người dùng
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu tạo tệp đính kèm cho bài nộp: " + ex.Message);
                throw;
            }
        }

    }
}
