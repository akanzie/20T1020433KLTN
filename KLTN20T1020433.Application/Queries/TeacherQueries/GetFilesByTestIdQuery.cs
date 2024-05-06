using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetFilesByTestIdQuery : IRequest<IEnumerable<GetTestFileResponse>>
    {
        public int TestId { get; set; }
    }
    public class GetFilesByTestIdQueryHandler : IRequestHandler<GetFilesByTestIdQuery, IEnumerable<GetTestFileResponse>>
    {
        private readonly ITestFileRepository _testFileDB;
        private readonly IMapper _mapper;

        public GetFilesByTestIdQueryHandler(ITestFileRepository testFileDB, IMapper mapper)
        {
            _testFileDB = testFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetTestFileResponse>> Handle(GetFilesByTestIdQuery request, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
