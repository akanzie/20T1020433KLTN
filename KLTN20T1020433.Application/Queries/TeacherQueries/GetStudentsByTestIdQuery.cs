using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Student;
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
        private readonly IStudentRepository _studentDB;

        public GetStudentsByTestIdQueryHandler(IStudentRepository studentDB)
        {
            _studentDB = studentDB;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByTestIdQuery request, CancellationToken cancellationToken)
        {
            var students = new List<GetStudentResponse>();
            int j = 0;
            for (int i = 0; i < 9; i++)
            {
                var student = new GetStudentResponse
                {
                    StudentId = $"20T102000{j++}",
                    FirstName = $"Kiệt{j++}",
                    LastName = $"Châu Anh",
                    Email = $"20T102000{j++}@husc.edu.vn"
                };
                students.Add(student);
            }
            return students;
        }
    }
}
