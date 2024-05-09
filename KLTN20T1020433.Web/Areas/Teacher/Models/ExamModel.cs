using KLTN20T1020433.Application.DTOs.TeacherDTOs;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class ExamModel
    {
        public int TestId { get; set; }
        public IEnumerable<GetExamResponse> Exams { get; set; }
    }
}
