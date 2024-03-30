using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Enum
{
    /// <summary>
    /// Trạng thái của bài nộp
    /// </summary>
    public enum SubmissionStatus
    {
        NotSubmitted,           // Chưa nộp bài
        Absent,                 // Thiếu (không nộp bài)
        Submitted,              // Đã nộp    
        LateSubmission,         // Đã nộp muộn
        PendingProcessing       // Đang chờ xử lý
    }
}
