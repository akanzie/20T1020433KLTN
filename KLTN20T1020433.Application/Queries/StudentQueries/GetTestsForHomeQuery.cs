using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Domain.Test;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetTestsForHomeQuery : IRequest<IEnumerable<GetTestBySearchResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string StudentId { get; set; }
    }
    public class GetTestsForHomeQueryHandler : IRequestHandler<GetTestsForHomeQuery, IEnumerable<GetTestBySearchResponse>>
    {
        private readonly ITestRepository _testDB;
        private readonly ITeacherRepository _teacherDB;
        private readonly IMapper _mapper;

        public GetTestsForHomeQueryHandler(ITestRepository testDB, IMapper mapper, ITeacherRepository teacherDB)
        {
            _testDB = testDB;
            _mapper = mapper;
            _teacherDB = teacherDB;
        }
        public async Task<IEnumerable<GetTestBySearchResponse>> Handle(GetTestsForHomeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tests = await _testDB.GetTestsForStudentHome(request.Page, request.PageSize, request.StudentId);
                if (tests != null && tests.Any())
                {
                    List<GetTestBySearchResponse> testResponse = new List<GetTestBySearchResponse>();
                    foreach (var item in tests)
                    {
                        var getTestResponse = _mapper.Map<GetTestBySearchResponse>(item);
                        var teacher = await _teacherDB.GetTeacherById(item.TeacherId);
                        getTestResponse.TestStatusDisplayName = Utils.GetTestStatusDisplayNameForStudent(item.Status);
                        getTestResponse.TeacherName = teacher.TeacherName;
                        testResponse.Add(getTestResponse);
                    }
                    return testResponse;
                }
                return new List<GetTestBySearchResponse>();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log và thông báo cho người dùng
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu tìm kiếm bài kiểm tra: " + ex.Message);
                throw;
            }
        }
    }
}
