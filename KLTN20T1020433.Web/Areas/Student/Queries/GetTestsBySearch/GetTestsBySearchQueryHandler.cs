using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Areas.Student.Queries.GetTestFilesByTestId;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetTestsBySearch
{
    public class GetTestsBySearchQueryHandler : IRequestHandler<GetTestsBySearchQuery, IEnumerable<GetTestByIdResponse>>
    {
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;

        public GetTestsBySearchQueryHandler(ITestRepository testDB, IMapper mapper)
        {
            _testDB = testDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetTestByIdResponse>> Handle(GetTestsBySearchQuery request, CancellationToken cancellationToken)
        {
            var tests = await _testDB.GetTestsOfStudent(request.Page, request.PageSize, request.StudentId, request.SearchValue, request.Type, request.Status, request.FromTime, request.ToTime);
            if (tests != null && !tests.Any())
            {
                List<GetTestByIdResponse> testResponse = new List<GetTestByIdResponse>();
                foreach (var item in tests)
                {
                    GetTestByIdResponse getTestResponse = _mapper.Map<GetTestByIdResponse>(item);
                    testResponse.Add(getTestResponse);
                }
                return testResponse;
            }
            return new List<GetTestByIdResponse>();
        }
    }
}
