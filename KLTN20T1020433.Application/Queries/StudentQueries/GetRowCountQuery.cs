using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetRowCountQuery : IRequest<int>
    {
        public string SearchValue { get; set; } = "";
        public string StudentId { get; set; }
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
            try
            {
                int rowCount = await _testDB.CountTestsOfStudent(request.StudentId, request.SearchValue, request.Status, request.Type, request.FromTime, request.ToTime);
                return rowCount;
            }
            catch (Exception ex)
            {                
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu lấy số lượng hàng: " + ex.Message);
                throw;
            }
        }

    }
}
