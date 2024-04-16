using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetSubmissionFileByIdQuery : IRequest<GetSubmissionFileResponse>
    {
        public Guid Id { get; set; }
    }
    public class GetSubmissionFileByIdQueryHandler : IRequestHandler<GetSubmissionFileByIdQuery, GetSubmissionFileResponse>
    {
        public Task<GetSubmissionFileResponse> Handle(GetSubmissionFileByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
