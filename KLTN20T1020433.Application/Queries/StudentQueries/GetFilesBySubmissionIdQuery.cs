using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetFilesBySubmissionIdQuery : IRequest<IEnumerable<GetSubmissionFileResponse>>
    {
        public int SubmissionId { get; set; }
    }
    public class GetSubmissionFilesBySubmissionIdQueryHandler : IRequestHandler<GetFilesBySubmissionIdQuery, IEnumerable<GetSubmissionFileResponse>>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetSubmissionFilesBySubmissionIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetSubmissionFileResponse>> Handle(GetFilesBySubmissionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SubmissionFiles = await _submissionFileDB.GetFilesBySubmissionId(request.SubmissionId);
                if (SubmissionFiles != null && SubmissionFiles.Any())
                {
                    List<GetSubmissionFileResponse> SubmissionResponse = new List<GetSubmissionFileResponse>();
                    foreach (var file in SubmissionFiles)
                    {
                        GetSubmissionFileResponse getSubmissionFileResponse = _mapper.Map<GetSubmissionFileResponse>(file);
                        SubmissionResponse.Add(getSubmissionFileResponse);
                    }
                    return SubmissionResponse;
                }
                return new List<GetSubmissionFileResponse>();
            }
            catch (Exception ex)
            {                
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu lấy danh sách tệp đính kèm: " + ex.Message);
                throw;
            }
        }

    }
}
