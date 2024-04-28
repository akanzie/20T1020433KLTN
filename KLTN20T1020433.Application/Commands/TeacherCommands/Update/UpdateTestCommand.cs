using AutoMapper;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Commands.TeacherCommands.Update
{
    public class UpdateTestCommand : IRequest<bool>
    {
        public int TestId { get; set; }
        public string Title { get; set; } = "";
        public string Instruction { get; set; } = "";
        public bool IsCheckIP { get; set; } = false;
        public bool IsConductedAtSchool { get; set; } = false;
        public bool CanSubmitLate { get; set; } = false;
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public string TeacherId { get; set; }

    }
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, bool>
    {
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;
        public UpdateTestCommandHandler(ITestRepository testDB, IMapper mapper)
        {
            _testDB = testDB;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            Test test = await _testDB.GetById(request.TestId);
            if (test != null)
            {
                test.Title = request.Title;
                test.Instruction = request.Instruction;
                test.IsCheckIP = request.IsCheckIP;
                test.IsConductedAtSchool = request.IsConductedAtSchool;
                test.StartTime = request.StartTime;
                test.EndTime = request.EndTime;
                test.LastUpdateTime = DateTime.Now;
                test.CanSubmitLate = request.CanSubmitLate;                
                if (await _testDB.Update(test))
                    return true;
            }
            return false;
        }
    }
}
