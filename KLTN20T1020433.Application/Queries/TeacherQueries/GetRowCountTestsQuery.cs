using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetRowCountTestsQuery : IRequest<int>
    {
        public string SearchValue { get; set; } = "";
        public string TeacherId { get; set; }
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;
    }
    public class GetRowCountTestsQueryHandler : IRequestHandler<GetRowCountTestsQuery, int>
    {
        private readonly ITestRepository _testDB;

        public GetRowCountTestsQueryHandler(ITestRepository testDB)
        {
            _testDB = testDB;

        }
        public async Task<int> Handle(GetRowCountTestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                int rowCount = await _testDB.CountTestsOfStudent(request.TeacherId, request.SearchValue, request.Type, request.FromTime, request.ToTime);

                return rowCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
