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
        Submitted,              //Đã nộp    
        Graded,                 //Đã chấm điểm
        LateSubmission,         //Đã nộp muộn
        PendingProcessing       //Đang chờ xử lý
    }
}
