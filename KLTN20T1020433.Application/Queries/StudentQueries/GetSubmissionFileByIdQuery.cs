using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.Services;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetSubmissionFileByIdQuery : IRequest<GetFileResponse?>
    {
        public Guid Id { get; set; }
    }
    public class GetSubmissionFileByIdQueryHandler : IRequestHandler<GetSubmissionFileByIdQuery, GetFileResponse?>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetSubmissionFileByIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;

            _mapper = mapper;
        }

        public async Task<GetFileResponse?> Handle(GetSubmissionFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var file = await _submissionFileDB.GetById(request.Id);
                if (file != null)
                {
                    GetFileResponse fileResponse = _mapper.Map<GetFileResponse>(file);
                    
                    return fileResponse;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log và thông báo cho người dùng
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu lấy tệp đính kèm: " + ex.Message);
                throw;
            }
        }

    }
}
