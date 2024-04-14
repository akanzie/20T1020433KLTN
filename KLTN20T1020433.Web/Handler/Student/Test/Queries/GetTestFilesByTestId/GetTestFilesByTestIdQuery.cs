using KLTN20T1020433.Web.Areas.Student.Models.TestModel;
using MediatR;

namespace KLTN20T1020433.Web.Handler.Student.Test.Queries.GetTestFilesByTestId
{
    public class GetTestFilesByTestIdQuery : IRequest<IEnumerable<GetTestFileResponse>>
    {
        public int TestId { get; set; }
    }
}
