using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.Services;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetFilesBySubmissionIdQuery : IRequest<IEnumerable<GetFileResponse>>
    {
        public int SubmissionId { get; set; }
    }
    public class GetSubmissionFilesBySubmissionIdQueryHandler : IRequestHandler<GetFilesBySubmissionIdQuery, IEnumerable<GetFileResponse>>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetSubmissionFilesBySubmissionIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetFileResponse>> Handle(GetFilesBySubmissionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SubmissionFiles = await _submissionFileDB.GetFilesBySubmissionId(request.SubmissionId);
                if (SubmissionFiles != null && SubmissionFiles.Any())
                {
                    List<GetFileResponse> SubmissionResponse = new List<GetFileResponse>();
                    foreach (var file in SubmissionFiles)
                    {
                        var getSubmissionFileResponse = _mapper.Map<GetFileResponse>(file);                        
                        SubmissionResponse.Add(getSubmissionFileResponse);
                    }
                    return SubmissionResponse;
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
