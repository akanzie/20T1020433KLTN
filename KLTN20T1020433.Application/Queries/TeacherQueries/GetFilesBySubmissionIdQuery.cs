using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetFilesBySubmissionIdQuery : IRequest<IEnumerable<GetSubmissionFileResponse>>
    {
        public int SubmissionId { get; set; }
    }
    public class GetFilesBySubmissionIdQueryHandler : IRequestHandler<GetFilesBySubmissionIdQuery, IEnumerable<GetSubmissionFileResponse>>
    {
        private readonly ISubmissionFileRepository _submissionFileDB;
        private readonly IMapper _mapper;

        public GetFilesBySubmissionIdQueryHandler(ISubmissionFileRepository submissionFileDB, IMapper mapper)
        {
            _submissionFileDB = submissionFileDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetSubmissionFileResponse>> Handle(GetFilesBySubmissionIdQuery request, CancellationToken cancellationToken)
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
    }
}
