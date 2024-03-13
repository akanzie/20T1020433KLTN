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
        /// <summary>
        /// Mã bài nộp
        /// </summary>
        public long SubmissionId { get; set; }
        /// <summary>
        /// Thời gian nộp
        /// </summary>
        public DateTime SubmitTime {  get; set; }
        /// <summary>
        /// Mã sinh viên
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// Mã kỳ thi
        /// </summary>
        public long ExamId { get; set; }
        /// <summary>
        /// Số điểm
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Địa chỉ IP nộp bài
        /// </summary>
        public String IPAddress {  get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public SubmissionStatus Status { get; set; }

    }
}
