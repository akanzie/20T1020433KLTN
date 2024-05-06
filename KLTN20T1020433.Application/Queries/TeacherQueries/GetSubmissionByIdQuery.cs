using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetSubmissionByIdQuery : IRequest<GetSubmissionResponse>
    {
        public int Id { get; set; }
    }
    public class GetSubmissionByIdQueryHandler : IRequestHandler<GetSubmissionByIdQuery, GetSubmissionResponse>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionByIdQueryHandler(ISubmissionRepository submissionDB, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionResponse> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var submission = await _submissionDB.GetById(request.Id);
                if (submission != null)
                {
                    GetSubmissionResponse submissionResponse = _mapper.Map<GetSubmissionResponse>(submission);
                    return submissionResponse;

                }
                return new GetSubmissionResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
