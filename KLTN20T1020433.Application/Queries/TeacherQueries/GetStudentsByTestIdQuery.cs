using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Student;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByTestIdQuery : IRequest<IEnumerable<GetStudentResponse>>
    {
        public int TestId { get; set; }
    }
    public class GetStudentsByTestIdQueryHandler : IRequestHandler<GetStudentsByTestIdQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly IStudentRepository _studentDB;
        private readonly IMapper _mapper;
        public GetStudentsByTestIdQueryHandler(ISubmissionRepository submissionDB, IStudentRepository studentDB, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _studentDB = studentDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByTestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var submissions = await _submissionDB.GetSubmissionsBySearch(1, 0, request.TestId, "", null);
                if (submissions != null && submissions.Any())
                {
                    List<GetStudentResponse> studentsResponse = new List<GetStudentResponse>();
                    foreach (var item in submissions)
                    {
                        var student = await _studentDB.GetStudentById(item.StudentId);
                        GetStudentResponse studentResponse = _mapper.Map<GetStudentResponse>(student);
                        studentsResponse.Add(studentResponse);
                    }
                    return studentsResponse;
                }
                return new List<GetStudentResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
