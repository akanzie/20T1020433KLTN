﻿using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Web.Models;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class StudentModel 
    {        
        public IEnumerable<GetStudentResponse> Students { get; set; }
    }
}
