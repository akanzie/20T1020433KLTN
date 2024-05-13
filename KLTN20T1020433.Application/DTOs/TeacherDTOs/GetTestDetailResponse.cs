using KLTN20T1020433.Domain.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{
    public class GetTestDetailResponse
    {
        public int TestId { get; set; }
        public string Title { get; set; } = "";
        public string Instruction { get; set; } = "";       
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public bool IsCheckIP { get; set; }
        public bool IsConductedAtSchool { get; set; }               
        public string TeacherName { get; set; }
    }
}
