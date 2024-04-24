using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetRowCountSubmissionsQuery : IRequest<int>
    {
        public string SearchValue { get; set; } = "";
        public int TestId { get; set; }        
        public SubmissionStatus? Status { get; set; } = null;  
    }
    public class GetRowCountSubmissionsQueryHandler : IRequestHandler<GetRowCountSubmissionsQuery, int>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissionDB;
        public GetRowCountSubmissionsQueryHandler(ITestRepository testDB, ISubmissionRepository submissionDB)
        {
            _testDB = testDB;
            _submissionDB = submissionDB;
        }
        public async Task<int> Handle(GetRowCountSubmissionsQuery request, CancellationToken cancellationToken)
        {

            int rowCount = await _submissionDB.CountSubmissions(request.TestId, request.SearchValue, request.Status);

            return rowCount;
        }
    }
}
