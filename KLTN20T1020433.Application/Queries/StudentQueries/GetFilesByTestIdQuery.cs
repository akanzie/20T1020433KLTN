using AutoMapper;
using KLTN20T1020433.Domain.Test;
using MediatR;
using System.Collections.Generic;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.Services;
using static System.Net.Mime.MediaTypeNames;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetFilesByTestIdQuery : IRequest<IEnumerable<GetFileResponse>>
    {
        public int TestId { get; set; }
        public DateTime? TestStartTime { get; set; } = null;
    }
    public class GetFilesByTestIdQueryHandler : IRequestHandler<GetFilesByTestIdQuery, IEnumerable<GetFileResponse>>
    {
        private readonly ITestFileRepository _testFileDB;
        private readonly IMapper _mapper;

        public GetFilesByTestIdQueryHandler(ITestFileRepository testFileDB, IMapper mapper)
        {
            _testFileDB = testFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetFileResponse>> Handle(GetFilesByTestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.TestStartTime > DateTime.Now && request.TestStartTime != null)
                {
                    return new List<GetFileResponse>();
                }
                var testFiles = await _testFileDB.GetFilesByTestId(request.TestId);
                if (testFiles != null && testFiles.Any())
                {
                    List<GetFileResponse> testResponse = new List<GetFileResponse>();
                    foreach (var file in testFiles)
                    {
                        GetFileResponse fileResponse = _mapper.Map<GetFileResponse>(file);
                        fileResponse.FileType = Path.GetExtension(file.FileName);
                        testResponse.Add(fileResponse);
                    }
                    return testResponse;
                }
                return new List<GetFileResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu lấy danh sách tệp đính kèm: " + ex.Message);
                throw;
            }
        }

    }
}
