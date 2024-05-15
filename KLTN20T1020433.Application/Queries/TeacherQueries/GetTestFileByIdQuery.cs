using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetTestFileByIdQuery : IRequest<GetFileResponse?>
    {
        public Guid Id { get; set; }
    }
    public class GetTestFileByIdQueryHandler : IRequestHandler<GetTestFileByIdQuery, GetFileResponse?>
    {
        private readonly ITestFileRepository _testFileDB;
        private readonly IMapper _mapper;

        public GetTestFileByIdQueryHandler(ITestFileRepository testFileDB, IMapper mapper)
        {
            _testFileDB = testFileDB;

            _mapper = mapper;
        }
        public async Task<GetFileResponse?> Handle(GetTestFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _testFileDB.GetById(request.Id);
                if (file != null)
                {
                    GetFileResponse fileResponse = _mapper.Map<GetFileResponse>(file);
                    fileResponse.FileType = Path.GetExtension(file.FileName);
                    return fileResponse;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
