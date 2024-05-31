using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Student;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateSubmissionCommand : IRequest<int>
    {
        public List<StudentSelection> Students { get; set; }
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

                foreach (var item in request.Students)
                {
                    if (studentIdSet.Contains(item.StudentId))
                    {
                        hasDuplicate = true;
                    }
                    else
                    {
                        var submissionOld = await _submissionDB.GetByTestIdAndStudentId(request.TestId, item.StudentId);
                        if (submissionOld == null)
                        {
                            var submission = new Submission
                            {
                                TestId = request.TestId,
                                StudentId = item.StudentId,
                                Status = SubmissionStatus.NotSubmitted
                            };
                            var student = await _studentDB.GetStudentById(item.StudentId);
                            if (student == null)
                            {
                                var names = Utils.ParseStudentName(item.StudentName);
                                await _studentDB.Add(new Student
                                {
                                    StudentId = item.StudentId,
                                    FirstName = names.FirstName,
                                    LastName = names.LastName
                                });
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
