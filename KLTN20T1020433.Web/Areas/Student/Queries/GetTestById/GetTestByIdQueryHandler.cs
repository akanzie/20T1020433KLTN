using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;
using System.Net.WebSockets;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetTest
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, GetTestByIdResponse>
    {
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;

        public GetTestByIdQueryHandler(ITestRepository testDB, IMapper mapper)
        {
            _testDB = testDB;
            _mapper = mapper;
        }

        public async Task<GetTestByIdResponse> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _testDB.GetById(request.Id);
            if (test != null)
            {
                GetTestByIdResponse testResponse = _mapper.Map<GetTestByIdResponse>(test);
                return testResponse;
            }
            return new GetTestByIdResponse();
        }
    }
}
