using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByTestIdQuery : IRequest<IEnumerable<GetStudentResponse>>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
        public int TestId { get; set; }
    }
    public class GetStudentsByTestIdQueryHandler : IRequestHandler<GetStudentsByTestIdQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public GetStudentsByTestIdQueryHandler(IMediator mediator, ISubmissionRepository submissionDB, ApiService apiService, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByTestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var submissions = await _submissionDB.GetSubmissionsBySearch(1, 0, request.TestId, "", null);
                if (submissions != null && submissions.Any())
                {
                    List<GetStudentResponse> studentsResponse = new List<GetStudentResponse>();
                    foreach (var item in submissions)
                    {

                        var student = await _mediator.Send(new GetStudentByIdQuery { StudentId = item.StudentId, GetTokenResponse = request.GetTokenResponse });
                        studentsResponse.Add(student);
                    }
                    return studentsResponse;
                }
                return new List<GetStudentResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
