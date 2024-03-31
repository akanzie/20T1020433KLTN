using KLTN20T102433.Domain.Enum;
using KLTN20T102433.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.Domain.Entities
{
    /// <summary>
    /// Bài nộp của sinh viên
    /// </summary>
    public class Submission 
    {
        public int SubmissionId { get; set; }
        public DateTime SubmitTime {  get; set; }
        public string StudentId { get; set; }
        public int TestId { get; set; }
        public String IPAddress {  get; set; }
        public SubmissionStatus Status { get; set; }

    }
}
