using AutoMapper;
using KLTN20T1020433.Domain.Test;
using MediatR;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Application.Services;
using static System.Net.Mime.MediaTypeNames;
using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetTestsBySearchQuery : GetTestsBySearchRequest, IRequest<IEnumerable<GetTestBySearchResponse>>
    {
        public string StudentId { get; set; }
    }
    public class GetTestsBySearchQueryHandler : IRequestHandler<GetTestsBySearchQuery, IEnumerable<GetTestBySearchResponse>>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissonDB;
        private readonly ITeacherRepository _teacherDB;
        private readonly IMapper _mapper;

        public GetTestsBySearchQueryHandler(ITestRepository testDB, ISubmissionRepository submissionDB, IMapper mapper, ITeacherRepository teacherDB)
        {
            _testDB = testDB;
            _mapper = mapper;
            _submissonDB = submissionDB;
            _teacherDB = teacherDB;
        }
        public async Task<IEnumerable<GetTestBySearchResponse>> Handle(GetTestsBySearchQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tests = await _testDB.GetTestsOfStudent(request.Page, request.PageSize, request.StudentId, request.SearchValue, request.Status, request.Type, request.FromTime, request.ToTime);
                if (tests != null && tests.Any())
                {
                    List<GetTestBySearchResponse> testResponse = new List<GetTestBySearchResponse>();
                    foreach (var item in tests)
                    {
                        
                            var submission = await _submissonDB.GetByTestIdAndStudentId(item.TestId, request.StudentId);
                            GetTestBySearchResponse getTestResponse = _mapper.Map<GetTestBySearchResponse>(item);
                            Teacher teacher = await _teacherDB.GetTeacherById(item.TeacherId);
                            getTestResponse.TestStatusDisplayName = Utils.GetTestStatusDisplayNameForStudent(item.Status);
                            getTestResponse.SubmissionStatus = submission.Status;
                            getTestResponse.SubmissionStatusDisplayName = Utils.GetSubmissionStatusDisplayName(submission.Status);
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
