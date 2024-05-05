using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;
using System.Net.WebSockets;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Application.Services;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetTestByIdQuery : IRequest<GetTestByIdResponse?>
    {
        public int Id { get; set; }
    }
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, GetTestByIdResponse?>
    {
        private readonly ITestRepository _testDB;
        private readonly ITeacherRepository _teacherDB;
        private readonly IMapper _mapper;

        public GetTestByIdQueryHandler(ITestRepository testDB, ITeacherRepository teacherDB, IMapper mapper)
        {
            _testDB = testDB;
            _mapper = mapper;
            _teacherDB = teacherDB;
        }

        public async Task<GetTestByIdResponse?> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _testDB.GetById(request.Id);
            if (test != null)
            {
                if (test.StartTime <= DateTime.Now || test.StartTime == null)
                {
                    GetTestByIdResponse testResponse = _mapper.Map<GetTestByIdResponse>(test);
                    var teacher = await _teacherDB.GetTeacherById(test.TeacherId);
                    testResponse.StatusDisplayName = Utils.GetTestStatusDisplayNameForStudent(test.Status);
                    testResponse.TeacherName = teacher!.TeacherName;
                    return testResponse;
                }
            }
            return null;
        }
    }
}
