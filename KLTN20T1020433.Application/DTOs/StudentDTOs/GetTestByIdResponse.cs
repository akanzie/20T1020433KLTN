﻿using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.DTOs.StudentDTOs
{

    public class GetTestByIdResponse
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public TestType TestType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsCheckIP { get; set; }
        public bool IsConductedAtSchool { get; set; }
        public TestStatus Status { get; set; }
        public string StatusDescription { get; set; }
        public string TeacherName { get; set; }
    }

}