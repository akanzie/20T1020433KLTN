using AutoMapper;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Student;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateSubmissionCommand : IRequest<int>
    {
        public string[] StudentIds { get; set; }
        public int TestId { get; set; }
    }
    public class CreateSubmissionCommandHandler : IRequestHandler<CreateSubmissionCommand, int>
    {
        private readonly IStudentRepository _studentDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;
        public CreateSubmissionCommandHandler(IStudentRepository studentDB, IMapper mapper, ISubmissionRepository submissionDB)
        {
            _studentDB = studentDB;
            _mapper = mapper;
            _submissionDB = submissionDB;
        }
        public async Task<int> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                HashSet<string> studentIdSet = new HashSet<string>();
                bool hasDuplicate = false;

                foreach (string item in request.StudentIds)
                {
                    if (studentIdSet.Contains(item))
                    {
                        hasDuplicate = true;
                    }
                    else
                    {
                        var submissionOld = await _submissionDB.GetByTestIdAndStudentId(request.TestId, item);
                        if (submissionOld == null)
                        {
                            var submission = new Submission
                            {
                                TestId = request.TestId,
                                StudentId = item,
                                Status = SubmissionStatus.NotSubmitted
                            };
                            var student = await _studentDB.GetStudentById(item);
                            if (student == null)
                            {
                                await _studentDB.Add(new Student { StudentId = item, FirstName = "A", LastName = "Nguyễn Văn" });
                            }
                            await _submissionDB.Add(submission);
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
