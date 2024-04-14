using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;
using System.Collections.Generic;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetTestFilesByTestId
{
    public class GetTestFilesByTestIdQueryHandler : IRequestHandler<GetTestFilesByTestIdQuery, IEnumerable<GetTestFileResponse>>
    {
        private readonly ITestFileRepository _testFileDB;
        private readonly IMapper _mapper;

        public GetTestFilesByTestIdQueryHandler(ITestFileRepository testFileDB, IMapper mapper)
        {
            _testFileDB = testFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetTestFileResponse>> Handle(GetTestFilesByTestIdQuery request, CancellationToken cancellationToken)
        {
            var testFiles = await _testFileDB.GetFilesByTestId(request.TestId);
            if (testFiles != null && testFiles.Any())
            {
                List<GetTestFileResponse> testResponse = new List<GetTestFileResponse>();
                foreach (var file in testFiles)
                {
                    GetTestFileResponse getTestFileResponse = _mapper.Map<GetTestFileResponse>(file);
                    testResponse.Add(getTestFileResponse);
                }
                return testResponse;
            }
            return new List<GetTestFileResponse>();
        }
    }
}
