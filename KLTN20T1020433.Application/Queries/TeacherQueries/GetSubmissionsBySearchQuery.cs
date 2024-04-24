using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
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
    public class GetSubmissionsBySearchQuery : PaginationSearchInput, IRequest<IEnumerable<GetSubmissionBySearchResponse>>
    {
        public SubmissionStatus? Status { get; set; } = null;
        public int TestId { get; set; }
    }
    public class GetSubmissionsBySearchQueryHandler : IRequestHandler<GetSubmissionsBySearchQuery, IEnumerable<GetSubmissionBySearchResponse>>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetSubmissionsBySearchQueryHandler(ITestRepository testDB, ISubmissionRepository submissionDB, IMapper mapper)
        {
            _testDB = testDB;
            _submissionDB = submissionDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetSubmissionBySearchResponse>> Handle(GetSubmissionsBySearchQuery request, CancellationToken cancellationToken)
        {
            var submissions = await _submissionDB.GetSubmissionsBySearch(request.Page, request.PageSize, request.TestId, request.SearchValue, request.Status);
            if (submissions != null && submissions.Any())
            {
                List<GetSubmissionBySearchResponse> testResponse = new List<GetSubmissionBySearchResponse>();
                foreach (var item in submissions)
                {
                    GetSubmissionBySearchResponse getTestResponse = _mapper.Map<GetSubmissionBySearchResponse>(item);
                    testResponse.Add(getTestResponse);
                }
                return testResponse;
            }
            return new List<GetSubmissionBySearchResponse>();
        }
    }
}
