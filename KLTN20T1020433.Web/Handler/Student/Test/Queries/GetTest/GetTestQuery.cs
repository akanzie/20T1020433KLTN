using KLTN20T1020433.Web.Areas.Student.Models.TestModel;
using MediatR;

namespace KLTN20T1020433.Web.Handler.Student.Test.Queries.GetTest
{
    public class GetTestQuery : IRequest<GetTestResponse>
    {
        public int Id { get; set; }
    }
}
