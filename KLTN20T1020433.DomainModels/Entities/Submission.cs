using _20T1020433KLTN.Domain.Enum;
using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Entities
{
    /// <summary>
    /// Bài nộp của sinh viên
    /// </summary>
    public class Submission 
    {
        public int SubmissionId { get; set; }
        public DateTime SubmitTime {  get; set; }
        public string StudentId { get; set; }
        public long TestId { get; set; }
        public String IPAddress {  get; set; }
        public SubmissionStatus Status { get; set; }

    }
}
