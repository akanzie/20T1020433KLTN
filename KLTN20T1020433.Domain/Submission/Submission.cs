
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
        public DateTime SubmittedTime { get; set; }
        public string StudentId { get; set; }
        public int TestId { get; set; }
        public String IPAddress { get; set; }
        public SubmissionStatus Status { get; set; }
        public string StatusDescription
        {
            get
            {
                if (Status == SubmissionStatus.NotSubmitted)
                {
                    return "Chưa nộp bài";
                }
                else if (Status == SubmissionStatus.PendingProcessing)
                {
                    return "Đang chờ xử lý";
                }
                else if (Status == SubmissionStatus.Submitted)
                {
                    return "Đã nộp";
                }
                else if (Status == SubmissionStatus.LateSubmission)
                {
                    return "Đã nộp muộn";
                }
                else
                    return "Thiếu";
            }
        }
    }
}
