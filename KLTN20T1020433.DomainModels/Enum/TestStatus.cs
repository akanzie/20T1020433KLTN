using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.Domain.Enum
{
    /// <summary>
    /// Trạng thái của kỳ thi
    /// </summary>
    public enum TestStatus
    {
        Upcoming, //Chưa bắt đầu        
        Ongoing,    //Đang diễn ra        
        Finished,  //Đã kết thúc
        Canceled   //Đã bị hủy
    }
}
