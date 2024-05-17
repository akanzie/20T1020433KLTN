using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Web.Models;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class StudentSearchResult : BasePaginationResult
    {      
        public IEnumerable<GetStudentResponse> Data { get; set; }        
    }
}
