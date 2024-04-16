
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KLTN20T1020433.Domain.Test
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
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;
        public bool IsCheckIP { get; set; }
        public bool IsConductedAtSchool { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastUpdateTime { get; set; } = null;
        private TestStatus _status;

        public TestStatus Status
        {
            get
            {
                if (_status == TestStatus.InProgress)
                {
                    if (DateTime.Now < StartTime)
                        return TestStatus.Upcoming;
                    else if (DateTime.Now > EndTime)
                        return TestStatus.Finished;
                    else
                        return TestStatus.Ongoing;
                }
                else
                {
                    return _status;
                }
            }
            set
            {
                _status = value;
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
                else if (Status == TestStatus.Creating)
                {
                    return "Đang tạo";
                }
                else
                    return "Đã bị hủy";
            }
        }

        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
