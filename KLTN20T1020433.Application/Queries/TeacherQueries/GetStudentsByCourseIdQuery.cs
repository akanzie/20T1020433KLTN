using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByCourseIdQuery : IRequest<IEnumerable<GetStudentResponse>>
    {
    }
    public class GetStudentsByCourseIdQueryHandler : IRequestHandler<GetStudentsByCourseIdQuery, IEnumerable<GetStudentResponse>>
    {
        public Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
