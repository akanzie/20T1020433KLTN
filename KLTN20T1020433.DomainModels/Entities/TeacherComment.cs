using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DomainModels.Entities
{
    /// <summary>
    /// Nhận xét của giảng viên
    /// </summary>
    public class TeacherComment
    {
        public int CommentId { get; set; }
        public string Body { get; set; } = "";
        public DateTime CreatedTime { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; } = "";
        public int TestId { get; set; }

    }
}
