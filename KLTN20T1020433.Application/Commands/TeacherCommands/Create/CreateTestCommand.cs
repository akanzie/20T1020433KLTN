using AutoMapper;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Create
{
    public class CreateTestCommand : IRequest<int>
    {
        public string Title { get; set; } = "";
        public string Instruction { get; set; } = "";
        public bool IsCheckIP { get; set; } = false;
        public bool IsConductedAtSchool { get; set; } = false;
        public bool CanSubmitLate { get; set; } = false;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Email { get; set; }
        public TestType TestType { get; set; }
        public TestStatus TestStatus { get; set; }
    }
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, int>
    {

        private readonly ITestRepository _testDB;
        private readonly ITeacherRepository _teacherDB;
        private readonly IMapper _mapper;
        public CreateTestCommandHandler(ITestRepository testDB, IMapper mapper, ITeacherRepository teacherDB)
        {
            _testDB = testDB;
            _mapper = mapper;
            _teacherDB = teacherDB;
        }
        public async Task<int> Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Test test = _mapper.Map<Test>(request);
                test.CreatedTime = DateTime.Now;
                int testId = await _testDB.Add(test);
                if (await _teacherDB.GetTeacherById(request.TeacherId) == null)
                {
                    Teacher teacher = new Teacher
                    {
                        TeacherId = request.TeacherId,
                        TeacherName = request.TeacherName,
                        Email = request.Email
                    };
                    await _teacherDB.Add(teacher);
                }
                return testId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
