using AutoMapper;
using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace KLTN20T1020433.Application.Commands.StudentCommands.Create
{
    public class CreateSubmissionFileCommand : IRequest<bool>
    {
        public IFormFile File { get; set; }
        public int SubmissionId { get; set; }
    }
    public class CreateSubmissionFileCommandHandler : IRequestHandler<CreateSubmissionFileCommand, bool>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly ITestRepository _testDB;
        private readonly FileConfig _fileOptions;
        public CreateSubmissionFileCommandHandler(ISubmissionFileRepository submissionFileDB, ITestRepository testDB, ISubmissionRepository submissionDB, IOptions<FileConfig> options)
        {
            _submissionFileDB = submissionFileDB;
            _submissionDB = submissionDB;
            _testDB = testDB;
            _fileOptions = options.Value;
        }
        public async Task<bool> Handle(CreateSubmissionFileCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }
            Guid id = Guid.NewGuid();
            string uniqueFileName = $"{id}_{request.File.FileName}";
            Submission submission = await _submissionDB.GetById(request.SubmissionId);
            Test test = await _testDB.GetById(submission.TestId);
            string directoryPath = Path.Combine(_fileOptions.FileStoragePath, test.Title, "Submission");
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
    }
}
