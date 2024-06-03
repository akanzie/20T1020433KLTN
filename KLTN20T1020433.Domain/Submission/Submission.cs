
using KLTN20T1020433.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Submission
{
    /// <summary>
    /// Bài nộp của sinh viên
    /// </summary>
    public class Submission
    {
        public int SubmissionId { get; set; }
        public DateTime? SubmitTime { get; set; } = null;
        public string StudentId { get; set; }
        public int TestId { get; set; }          
        public SubmissionStatus Status { get; set; }        
    }
}
