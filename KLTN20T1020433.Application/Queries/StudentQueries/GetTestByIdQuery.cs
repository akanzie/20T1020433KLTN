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
            try
            {
                var test = await _testDB.GetById(request.Id);
                if (test != null)
                {

                    GetTestByIdResponse testResponse = _mapper.Map<GetTestByIdResponse>(test);
                    var teacher = await _teacherDB.GetTeacherById(test.TeacherId);
                    testResponse.StatusDisplayName = Utils.GetTestStatusDisplayNameForStudent(test.Status);
                    testResponse.TeacherName = teacher!.TeacherName;
                    if (test.StartTime >= DateTime.Now && test.StartTime != null)
                    {
                        testResponse.Instruction = "";
                    }
                    return testResponse;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log và thông báo cho người dùng
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu lấy thông tin bài kiểm tra: " + ex.Message);
                throw;
            }
        }

    }
}
