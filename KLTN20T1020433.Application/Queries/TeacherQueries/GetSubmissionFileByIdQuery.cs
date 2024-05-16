using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetSubmissionFileByIdQuery : IRequest<GetFileResponse?>
    {
        public Guid Id { get; set; }
    }
    public class GetSubmissionFileByIdQueryHandler : IRequestHandler<GetSubmissionFileByIdQuery, GetFileResponse?>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionFileByIdQueryHandler(ISubmissionFileRepository submissionFileDB, ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _submissionDB = submissionDB;
            _mapper = mapper;
        }

        public async Task<GetFileResponse?> Handle(GetSubmissionFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _submissionFileDB.GetById(request.Id);
                var submission = await _submissionDB.GetById(file.SubmissionId);
                if (submission.Status != SubmissionStatus.NotSubmitted)
                {
                    if (file != null)
                    {
                        GetFileResponse fileResponse = _mapper.Map<GetFileResponse>(file);
                        fileResponse.FileType = Path.GetExtension(file.FileName);
                        return fileResponse;
                    }
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
