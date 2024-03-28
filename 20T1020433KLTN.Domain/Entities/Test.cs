using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Domain.Enum;
using _20T1020433KLTN.Infrastructure.Entities;

namespace _20T1020433KLTN.Domain.Entities
{
    /// <summary>
    /// Kỳ thi
    /// </summary>
    public class Test
    {

        public int TestId { get; set; }
        public string Title { get; set; } = "";
        public string Instructions { get; set; } = "";
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
        
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
