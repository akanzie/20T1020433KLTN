using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Domain.Test;

using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetTestsBySearchQuery : GetTestsBySearchRequest, IRequest<IEnumerable<GetTestBySearchResponse>>
    {
        public string TeacherId { get; set; }
    }
    public class GetTestsBySearchQueryHandler : IRequestHandler<GetTestsBySearchQuery, IEnumerable<GetTestBySearchResponse>>
    {
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherDB;
        private readonly ISubmissionRepository _submissionDB;
        public GetTestsBySearchQueryHandler(ITestRepository testDB, ITeacherRepository teacherDB, ISubmissionRepository submissionDB, IMapper mapper)
        {
            _testDB = testDB;
            _teacherDB = teacherDB;
            _submissionDB = submissionDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetTestBySearchResponse>> Handle(GetTestsBySearchQuery request, CancellationToken cancellationToken)
        {
            var tests = await _testDB.GetTestsOfTeacher(request.Page, request.PageSize, request.TeacherId, request.SearchValue, request.Type, request.Status, request.FromTime, request.ToTime);
            if (tests != null && tests.Any())
            {
                List<GetTestBySearchResponse> testResponse = new List<GetTestBySearchResponse>();
                foreach (var item in tests)
                {
                    GetTestBySearchResponse getTestResponse = _mapper.Map<GetTestBySearchResponse>(item);
                    Teacher teacher = await _teacherDB.GetTeacherById(item.TeacherId);
                    getTestResponse.TeacherName = teacher.TeacherName;
                    getTestResponse.StatusDisplayName = Utils.GetTestStatusDisplayNameForTeacher(item.Status);
                    int countStudents = await _submissionDB.CountSubmissions(item.TestId);
                    getTestResponse.CountStudents = countStudents;
                    testResponse.Add(getTestResponse);
                }
                return testResponse;
            }
            return new List<GetTestBySearchResponse>();
        }
    }
}
