using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetSubmissionsBySearchQuery : PaginationSearchInput, IRequest<IEnumerable<GetSubmissionBySearchResponse>>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
        public SubmissionStatus? Status { get; set; } = null;
        public int TestId { get; set; }
    }
    public class GetSubmissionsBySearchQueryHandler : IRequestHandler<GetSubmissionsBySearchQuery, IEnumerable<GetSubmissionBySearchResponse>>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetSubmissionsBySearchQueryHandler(IMediator mediator, ISubmissionFileRepository submissionFileDB, ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _submissionDB = submissionDB;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<IEnumerable<GetSubmissionBySearchResponse>> Handle(GetSubmissionsBySearchQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var submissions = await _submissionDB.GetSubmissionsBySearch(request.Page, request.PageSize, request.TestId, request.SearchValue, request.Status);
                if (submissions != null && submissions.Any())
                {
                    List<GetSubmissionBySearchResponse> testResponse = new List<GetSubmissionBySearchResponse>();
                    foreach (var item in submissions)
                    {
                        GetSubmissionBySearchResponse getSubmissionResponse = _mapper.Map<GetSubmissionBySearchResponse>(item);
                        var student = await _mediator.Send(new GetStudentByIdQuery { StudentId = item.StudentId, GetTokenResponse = request.GetTokenResponse });                        
                        getSubmissionResponse.StudentName = $"{student.LastName} {student.FirstName}";
                        getSubmissionResponse.StatusDisplayName = Utils.GetSubmissionStatusDisplayName(item.Status);
                        int filesCount = await _submissionFileDB.CountFilesBySubmissionId(item.SubmissionId);
                        getSubmissionResponse.FilesCount = filesCount;
                        testResponse.Add(getSubmissionResponse);
                    }
                    return testResponse;
                }
                return new List<GetSubmissionBySearchResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
