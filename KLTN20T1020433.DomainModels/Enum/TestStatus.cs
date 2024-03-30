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
    public enum TestStatus
    {
        All,
        Upcoming, //Chưa bắt đầu        
        Ongoing,    //Đang diễn ra        
        Finished,  //Đã kết thúc
        Cancelled   //Đã bị hủy
    }
}
