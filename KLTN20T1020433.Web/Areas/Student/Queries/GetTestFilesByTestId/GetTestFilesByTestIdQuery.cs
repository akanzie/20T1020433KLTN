using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetTestFilesByTestId
{
    public class GetTestFilesByTestIdQuery : IRequest<IEnumerable<GetTestFileResponse>>
    {
        public int TestId { get; set; }
    }
}
