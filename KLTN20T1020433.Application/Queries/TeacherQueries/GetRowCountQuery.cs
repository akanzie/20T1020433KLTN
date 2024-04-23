using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetRowCountQuery : IRequest<int>
    {
        public string SearchValue { get; set; } = "";
        public string TeacherId { get; set; }
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;
    }
    public class GetRowCountQueryHandler : IRequestHandler<GetRowCountQuery, int>
    {
        private readonly ITestRepository _testDB;
        

        public GetRowCountQueryHandler(ITestRepository testDB)
        {
            _testDB = testDB;
            
        }
        public async Task<int> Handle(GetRowCountQuery request, CancellationToken cancellationToken)
        {

            int rowCount = await _testDB.CountTestsOfStudent(request.TeacherId, request.SearchValue, request.Type, request.FromTime, request.ToTime);

            return rowCount;
        }
    }
}
