using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN20T1020433.DomainModels.Enum;
using KLTN20T1020433.Infrastructure.Entities;

namespace KLTN20T1020433.DomainModels.Entities
{
    /// <summary>
    /// Kỳ thi
    /// </summary>
    public class Test
    {

        public int TestId { get; set; }
        public string Title { get; set; } = "";
        public string Instruction { get; set; } = "";
        public TestType TestType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCheckIP { get; set; }
        public bool IsConductedAtSchool { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public TestStatus Status {
            get
            {
                if (DateTime.Now < StartTime)
                    return TestStatus.Upcoming;
                else if (DateTime.Now > EndTime)
                    return TestStatus.Finished;
                else
                    return TestStatus.Ongoing;
            }
        }
        public string StatusDescription
        {
            get
            {
                if (Status == TestStatus.Upcoming)
                {
                    return "Chưa bắt đầu";
                }
                else if (Status == TestStatus.Ongoing)
                {
                    return "Đang diễn ra";
                }
                else if (Status == TestStatus.Finished)
                {
                    return "Đã kết thúc";
                }
                else
                    return "Đã bị hủy";
            }
        }


        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
