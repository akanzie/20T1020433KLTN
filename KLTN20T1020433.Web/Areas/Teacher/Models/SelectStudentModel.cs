﻿using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Queries.TeacherQueries;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class SelectStudentModel
    {
        public int TestId { get; set; }   
        public int Semester {  get; set; }
        public string SchoolYear { get; set; }
        public Module Module { get; set; }
        public IEnumerable<SchoolYear> SchoolYears { get; set; }
    }
}