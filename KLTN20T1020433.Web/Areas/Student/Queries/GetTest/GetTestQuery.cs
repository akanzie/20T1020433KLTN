using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetTest
{
    public class GetTestQuery : IRequest<GetTestResponse>
    {
        public int Id { get; set; }
    }
}
