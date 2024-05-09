using KLTN20T1020433.Application.DTOs.TeacherDTOs;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class CourseModel
    {
        public int TestId { get; set; }
        public IEnumerable<GetCourseResponse> Courses { get; set; }
    }
}
