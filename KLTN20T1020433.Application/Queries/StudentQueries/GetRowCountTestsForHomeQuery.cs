using KLTN20T1020433.Domain.Test;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetRowCountTestsForHomeQuery : IRequest<int>
    {

        public string StudentId { get; set; }

    }
    public class GetRowCountTestsForHomeQueryHandler : IRequestHandler<GetRowCountTestsForHomeQuery, int>
    {
        private readonly ITestRepository _testDB;

        public GetRowCountTestsForHomeQueryHandler(ITestRepository testDB)
        {
            _testDB = testDB;

        }
        public async Task<int> Handle(GetRowCountTestsForHomeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                int rowCount = await _testDB.CountTestsForStudentHome(request.StudentId);
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
