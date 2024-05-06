using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetSubmissionFileByIdQuery : IRequest<GetSubmissionFileResponse?>
    {
        public Guid Id { get; set; }
    }
    public class GetSubmissionFileByIdQueryHandler : IRequestHandler<GetSubmissionFileByIdQuery, GetSubmissionFileResponse?>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetSubmissionFileByIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;

            _mapper = mapper;
        }

        public async Task<GetSubmissionFileResponse?> Handle(GetSubmissionFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _submissionFileDB.GetById(request.Id);
                if (file != null)
                {
                    GetSubmissionFileResponse fileResponse = _mapper.Map<GetSubmissionFileResponse>(file);

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
