using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{
    public class GetExamScheduleResponse
    {
        public string CourseName { get; set; }
        public string ExamDate { get; set; }
        public int ExamDuration { get; set; }
    }
}
