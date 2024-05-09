using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{
    public class GetCourseResponse
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int StudentCount { get; set; }

    }
}
