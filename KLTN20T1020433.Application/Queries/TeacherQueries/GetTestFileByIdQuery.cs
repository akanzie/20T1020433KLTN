using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetTestFileByIdQuery : IRequest<GetTestFileResponse>
    {
        public Guid Id { get; set; }
    }
    public class GetTestFileByIdQueryHandler : IRequestHandler<GetTestFileByIdQuery, GetTestFileResponse>
    {
        private readonly ITestFileRepository _testFileDB;
        private readonly IMapper _mapper;

        public GetTestFileByIdQueryHandler(ITestFileRepository testFileDB, IMapper mapper)
        {
            _testFileDB = testFileDB;

            _mapper = mapper;
        }
        public async Task<GetTestFileResponse> Handle(GetTestFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _testFileDB.GetById(request.Id);
                if (file != null)
                {
                    GetTestFileResponse fileResponse = _mapper.Map<GetTestFileResponse>(file);
                    return fileResponse;
                }
                return new GetTestFileResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
