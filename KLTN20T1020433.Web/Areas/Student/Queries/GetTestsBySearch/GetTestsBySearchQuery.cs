using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Student.Queries.GetTestsBySearch
{
    public class GetTestsBySearchQuery : IRequest<IEnumerable<GetTestByIdResponse>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public string SearchValue { get; set; } = "";
        public string StudentId { get; set; }
        public TestType? Type { get; set; } = null;
        public TestStatus? Status { get; set; } = null;
        public DateTime? FromTime { get; set; } = null;
        public DateTime? ToTime { get; set; } = null;
    }
}
