using KLTN20T1020433.Application.DTOs.TeacherDTOs;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class CourseModel
    {
        public IEnumerable<GetCourseResponse> Courses { get; set; }
    }
}
