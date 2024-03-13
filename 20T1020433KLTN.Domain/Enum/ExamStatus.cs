using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Enum
{
    /// <summary>
    /// Trạng thái của kỳ thi
    /// </summary>
    public enum ExamStatus
    {   All,    
        NotStarted, //Chưa bắt đầu        
        Ongoing,    //Đang diễn ra        
        Completed,  //Đã hoàn thành        
        Cancelled   //Đã bị hủy
    }
}
