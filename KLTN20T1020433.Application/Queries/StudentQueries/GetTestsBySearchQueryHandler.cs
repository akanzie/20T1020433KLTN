using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;
using KLTN20T1020433.Application.DTOs;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetTestsBySearchQuery : GetTestsBySearchRequest, IRequest<IEnumerable<GetTestBySearchResponse>>
    {
        public string StudentId { get; set; }
    }
    public class GetTestsBySearchQueryHandler : IRequestHandler<GetTestsBySearchQuery, IEnumerable<GetTestBySearchResponse>>
    {
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;

        public GetTestsBySearchQueryHandler(ITestRepository testDB, IMapper mapper)
        {
            _testDB = testDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetTestBySearchResponse>> Handle(GetTestsBySearchQuery request, CancellationToken cancellationToken)
        {
            var tests = await _testDB.GetTestsOfStudent(request.Page, request.PageSize, request.StudentId, request.SearchValue, request.Type, request.Status, request.FromTime, request.ToTime);
            if (tests != null && !tests.Any())
            {
                List<GetTestBySearchResponse> testResponse = new List<GetTestBySearchResponse>();
                foreach (var item in tests)
                {
                    GetTestBySearchResponse getTestResponse = _mapper.Map<GetTestBySearchResponse>(item);
                    testResponse.Add(getTestResponse);
                }
                return testResponse;
            }
            return new List<GetTestBySearchResponse>();
        }
    }
}
