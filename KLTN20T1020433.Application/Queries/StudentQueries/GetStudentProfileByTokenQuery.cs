using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetStudentProfileByTokenQuery : IRequest<GetStudentProfileResponse>
    {
        public string Token { get; set; }
    }
    public class GetStudentProfileByTokenQueryHandler : IRequestHandler<GetStudentProfileByTokenQuery, GetStudentProfileResponse>
    {
        public Task<GetStudentProfileResponse> Handle(GetStudentProfileByTokenQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
