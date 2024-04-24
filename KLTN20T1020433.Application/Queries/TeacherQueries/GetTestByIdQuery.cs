using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetTestByIdQuery : IRequest<GetTestByIdResponse>
    {
        public int Id { get; set; }
        public string TeacherID { get; set; }
    }
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
                if (request.TeacherID == test.TeacherId)
                {
                    GetTestByIdResponse testResponse = _mapper.Map<GetTestByIdResponse>(test);
                    return testResponse;
                }
            }
            return new GetTestByIdResponse();
        }
    }
}
