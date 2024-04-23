﻿using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;
using System.Net.WebSockets;
using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetTestByIdQuery : IRequest<GetTestByIdResponse>
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
    }
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, GetTestByIdResponse>
    {
        private readonly ITestRepository _testDB;
        private readonly ISubmissionRepository _submissionDB;
        private readonly IMapper _mapper;

        public GetTestByIdQueryHandler(ITestRepository testDB, ISubmissionRepository submissionDB, IMapper mapper)
        {
            _testDB = testDB;
            _submissionDB = submissionDB;
            _mapper = mapper;
        }

        public async Task<GetTestByIdResponse> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            Submission? submission = await _submissionDB.GetByTestIdAndStudentId(request.Id, request.StudentId);
            if (submission != null)
            {
                var test = await _testDB.GetById(request.Id);
                if (test != null)
                {
                    if (test.StartTime <= DateTime.Now)
                    {
                        GetTestByIdResponse testResponse = _mapper.Map<GetTestByIdResponse>(test);
                        return testResponse;
                    }
                }
            }
            return new GetTestByIdResponse();
        }
    }
}
