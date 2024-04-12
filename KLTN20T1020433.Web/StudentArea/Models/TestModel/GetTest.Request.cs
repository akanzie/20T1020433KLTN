
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Web.StudentArea.Models.TestModel   
{
    public class GetTestReq : IRequest<Test>
    {
        public int Id { get; set; }
    }
}
