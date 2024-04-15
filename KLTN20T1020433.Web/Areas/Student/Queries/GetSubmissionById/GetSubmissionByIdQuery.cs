using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionById
{
    public class GetSubmissionByIdQuery : IRequest<GetSubmissionResponse>    
    { 
        public int Id { get; set; }
    }
}
