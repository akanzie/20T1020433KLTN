using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetFilesBySubmissionIdQuery : IRequest<IEnumerable<GetSubmissionFileResponse>>
    {
        public int SubmissionId { get; set; }
    }
    public class GetSubmissionFilesBySubmissionIdQueryHandler : IRequestHandler<GetFilesBySubmissionIdQuery, IEnumerable<GetSubmissionFileResponse>>
    {
        public Task<IEnumerable<GetSubmissionFileResponse>> Handle(GetFilesBySubmissionIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
