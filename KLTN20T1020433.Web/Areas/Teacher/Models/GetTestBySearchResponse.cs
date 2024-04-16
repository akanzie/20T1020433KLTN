using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class GetTestBySearchResponse
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string StatusDescription { get; set; }
        public string TeacherName { get; set; }
    }
}
