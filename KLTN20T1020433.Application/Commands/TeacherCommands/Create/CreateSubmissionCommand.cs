using AutoMapper;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateSubmissionCommand : IRequest<string>
    {
        public string[] StudentIds { get; set; }
        public int TestId { get; set; }
    }
    public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, string>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;
        public CreateSubmissionCommandHandler(ITestRepository testDB, IMapper mapper, ISubmissionRepository submissionDB)
        {
            _testDB = testDB;
            _mapper = mapper;
            _submissionDB = submissionDB;
        }
        public async Task<string> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (string item in request.StudentIds)
                {
                    var submission = new Submission
                    {
                        TestId = request.TestId,
                        StudentId = item,
                        Status = SubmissionStatus.NotSubmitted
                    };
                    await _submissionDB.Add(submission);
                }
                return "Thêm sinh viên tham gia thành công";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
