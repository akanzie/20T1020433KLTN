using AutoMapper;
using KLTN20T1020433.Domain.Test;
using MediatR;
using System.Collections.Generic;
using KLTN20T1020433.Application.DTOs;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetFilesByTestIdQuery : IRequest<IEnumerable<GetTestFileResponse>>
    {
        public int TestId { get; set; }
    }
    public class GetTestFilesByTestIdQueryHandler : IRequestHandler<GetFilesByTestIdQuery, IEnumerable<GetTestFileResponse>>
    {
        private readonly ITestFileRepository _testFileDB;
        private readonly IMapper _mapper;

        public GetTestFilesByTestIdQueryHandler(ITestFileRepository testFileDB, IMapper mapper)
        {
            _testFileDB = testFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetTestFileResponse>> Handle(GetFilesByTestIdQuery request, CancellationToken cancellationToken)
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
