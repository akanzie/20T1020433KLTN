using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Teacher.Commands.Delete
{
    public class DeleteTestCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, string>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissionDB;
        public DeleteTestCommandHandler(ITestRepository testDB, ISubmissionRepository submissionDB)
        {
            _testDB = testDB;
            _submissionDB = submissionDB;
        }
        public async Task<string> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var test = await _testDB.GetById(request.Id);
                if (test == null)
                {
                    return ErrorMessages.TestNotFound;
                }
                string statuses = $"{SubmissionStatus.Submitted},{SubmissionStatus.LateSubmission},{SubmissionStatus.PendingProcessing}";

                var submissionCount = await _submissionDB.CountSubmissions(request.Id, "", statuses);
                if (test.Status == TestStatus.Finished && submissionCount <= 0)
                {
                    return ErrorMessages.CannotDeleteFinishedTest;
                }
                await _testDB.Delete(request.Id);
                return $"Xóa kỳ thi: {test.Title} thành công.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
