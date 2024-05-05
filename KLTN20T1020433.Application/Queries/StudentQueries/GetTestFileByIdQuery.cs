using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetTestFileByIdQuery : IRequest<GetTestFileResponse?>
    {
        public Guid Id { get; set; }
    }
    public class GetTestFileByIdQueryHandler : IRequestHandler<GetTestFileByIdQuery, GetTestFileResponse?>
    {
        private readonly ITestFileRepository _testFileDB;
        private readonly IMapper _mapper;

        public GetTestFileByIdQueryHandler(ITestFileRepository testFileDB, IMapper mapper)
        {
            _testFileDB = testFileDB;

            _mapper = mapper;
        }
        public async Task<GetTestFileResponse?> Handle(GetTestFileByIdQuery request, CancellationToken cancellationToken)
        {
            var file = await _testFileDB.GetById(request.Id);
            if (file != null)
            {
                GetTestFileResponse fileResponse = _mapper.Map<GetTestFileResponse>(file);
                return fileResponse;
            }
            return null;
        }
    }
}
