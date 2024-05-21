using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateCommentCommand : IRequest<string>
    {
        public string Body { get; set; }
        public int SubmissionId { get; set; }
        public string TeacherId { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, string>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly ICommentRepository _commentDB;

        public CreateCommentCommandHandler(ITestRepository testDB, ISubmissionRepository submissionDB, ICommentRepository commentDB)
        {
            _commentDB = commentDB;
            _testDB = testDB;
            _submissionDB = submissionDB;
        }
        public async Task<string> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var submission = await _submissionDB.GetById(request.SubmissionId);
                if (submission == null)
                {
                    return ErrorMessages.GeneralError;
                }
                var test = await _testDB.GetById(submission.TestId);
                if (test == null)
                {
                    return ErrorMessages.GeneralError;
                }
                if (test.TeacherId == request.TeacherId)
                {
                    var comment = new Comment
                    {
                        Body = request.Body,
                        TeacherId = request.TeacherId,
                        SubmissionId = request.SubmissionId,
                        CommentedTime = DateTime.Now,
                    };
                    await _commentDB.Add(comment);
                    return SuccessMessages.CommentSuccess;
                }
                return ErrorMessages.GeneralError;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
