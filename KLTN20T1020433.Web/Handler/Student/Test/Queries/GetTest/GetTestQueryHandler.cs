using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models.TestModel;
using MediatR;
using System.Net.WebSockets;

namespace KLTN20T1020433.Web.Handler.Student.Test.Queries.GetTest
{
    public class GetTestQueryHandler : IRequestHandler<GetTestQuery, GetTestResponse>
    {
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;

        public GetTestQueryHandler(ITestRepository testDB, IMapper mapper)
        {
            _testDB = testDB;
            _mapper = mapper;
        }        

        public async Task<GetTestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            var test = await _testDB.GetById(request.Id);            
            if (test != null)
            {
                GetTestResponse testResponse = _mapper.Map<GetTestResponse>(test);
                return testResponse;
            }                
            return new GetTestResponse();
        }
    }
}
