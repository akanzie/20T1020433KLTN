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
    public class GetSubmissionFilesByTestIdQuery : IRequest<IEnumerable<GetFileResponse>>
    {
        public int TestId { get; set; }

    }
    public class GetSubmissionFilesByTestIdQueryHandler : IRequestHandler<GetSubmissionFilesByTestIdQuery, IEnumerable<GetFileResponse>>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetSubmissionFilesByTestIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetFileResponse>> Handle(GetSubmissionFilesByTestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SubmissionFiles = await _submissionFileDB.GetFilesByTestId(request.TestId);
                if (SubmissionFiles != null && SubmissionFiles.Any())
                {
                    List<GetFileResponse> SubmissionResponse = new List<GetFileResponse>();
                    foreach (var file in SubmissionFiles)
                    {                        
                        GetFileResponse fileResponse = _mapper.Map<GetFileResponse>(file);
                        fileResponse.FileType = Path.GetExtension(file.FileName);
                        SubmissionResponse.Add(fileResponse);
                    }
                    return SubmissionResponse;
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
