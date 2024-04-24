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
    public class GetRowCountSubmissionQuery : IRequest<int>
    {
        public string SearchValue { get; set; } = "";
        public int TestId { get; set; }        
        public SubmissionStatus? Status { get; set; } = null;  
    }
    public class GetRowCountSubmissionQueryHandler : IRequestHandler<GetRowCountSubmissionQuery, int>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissionDB;
        public GetRowCountSubmissionQueryHandler(ITestRepository testDB, ISubmissionRepository submissionDB)
        {
            _testDB = testDB;
            _submissionDB = submissionDB;
        }
        public async Task<int> Handle(GetRowCountSubmissionQuery request, CancellationToken cancellationToken)
        {

            int rowCount = await _submissionDB.CountSubmissions(request.TestId, request.SearchValue, request.Status);

            return rowCount;
        }
    }
}
