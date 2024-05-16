using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetFilesBySubmissionIdQuery : IRequest<IEnumerable<GetFileResponse>>
    {
        public int SubmissionId { get; set; }
        public SubmissionStatus Status { get; set; }
    }
    public class GetFilesBySubmissionIdQueryHandler : IRequestHandler<GetFilesBySubmissionIdQuery, IEnumerable<GetFileResponse>>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetFilesBySubmissionIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetFileResponse>> Handle(GetFilesBySubmissionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Status != SubmissionStatus.NotSubmitted)
                {
                    var SubmissionFiles = await _submissionFileDB.GetFilesBySubmissionId(request.SubmissionId);
                    if (SubmissionFiles != null && SubmissionFiles.Any())
                    {
                        List<GetFileResponse> SubmissionResponse = new List<GetFileResponse>();
                        foreach (var file in SubmissionFiles)
                        {
                            var fileResponse = _mapper.Map<GetFileResponse>(file);
                            fileResponse.FileType = Path.GetExtension(file.FileName);
                            SubmissionResponse.Add(fileResponse);
                        }
                        return SubmissionResponse;
                    }
                }
                return new List<GetFileResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
