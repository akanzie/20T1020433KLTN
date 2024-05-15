using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Student;
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
    public class GetSubmissionByIdQuery : IRequest<GetSubmissionResponse?>
    {
        public int Id { get; set; }
    }
    public class GetSubmissionByIdQueryHandler : IRequestHandler<GetSubmissionByIdQuery, GetSubmissionResponse?>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly IStudentRepository _studentDB;
        private readonly IMapper _mapper;

        public GetSubmissionByIdQueryHandler(ISubmissionRepository submissionDB, IStudentRepository studentDB, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _studentDB = studentDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionResponse?> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var submission = await _submissionDB.GetById(request.Id);
                if (submission != null)
                {
                    var submissionResponse = _mapper.Map<GetSubmissionResponse>(submission);
                    var student = await _studentDB.GetStudentById(submission.StudentId);
                    submissionResponse.StudentName = $"{student.LastName} {student.FirstName}";
                    submissionResponse.StatusDisplayName = Utils.GetSubmissionStatusDisplayName(submission.Status);
                    return submissionResponse;

                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}
