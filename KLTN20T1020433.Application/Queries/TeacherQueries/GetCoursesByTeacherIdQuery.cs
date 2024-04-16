using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetCoursesByTeacherIdQuery : IRequest<IEnumerable<GetCourseResponse>>
    {
        public string TeacherId { get; set; }
    }
    public class GetCoursesByTeacherIdQueryHandler : IRequestHandler<GetCoursesByTeacherIdQuery, IEnumerable<GetCourseResponse>>
    {
        public Task<IEnumerable<GetCourseResponse>> Handle(GetCoursesByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
