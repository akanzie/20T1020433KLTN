using AutoMapper;
using KLTN20T1020433.Domain.Test;
using MediatR;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Domain.Teacher;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetTestsBySearchQuery : GetTestsBySearchRequest, IRequest<IEnumerable<GetTestBySearchResponse>>
    {
        public string StudentId { get; set; }
    }
    public class GetTestsBySearchQueryHandler : IRequestHandler<GetTestsBySearchQuery, IEnumerable<GetTestBySearchResponse>>
    {
        private readonly ITestRepository _testDB;
        private readonly ITeacherRepository _teacherDB;
        private readonly IMapper _mapper;

        public GetTestsBySearchQueryHandler(ITestRepository testDB, IMapper mapper, ITeacherRepository teacherDB)
        {
            _testDB = testDB;
            _mapper = mapper;
            _teacherDB = teacherDB;
        }
        public async Task<IEnumerable<GetTestBySearchResponse>> Handle(GetTestsBySearchQuery request, CancellationToken cancellationToken)
        {
            var tests = await _testDB.GetTestsOfStudent(request.Page, request.PageSize, request.StudentId, request.SearchValue, request.Type, request.FromTime, request.ToTime);
            if (tests != null && tests.Any())
            {
                List<GetTestBySearchResponse> testResponse = new List<GetTestBySearchResponse>();
                foreach (var item in tests)
                {
                    if (item.Status == request.Status|| request.Status == null)
                    {
                        GetTestBySearchResponse getTestResponse = _mapper.Map<GetTestBySearchResponse>(item);
                        Teacher teacher = await _teacherDB.GetTeacherById(item.TeacherId);                     
                        getTestResponse.TeacherName = teacher.TeacherName;                    
                        testResponse.Add(getTestResponse);
                    }
                }
                return testResponse;
            }
            return new List<GetTestBySearchResponse>();
        }
    }
}
