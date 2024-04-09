using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DomainModels.Enum
{
    /// <summary>
    /// Trạng thái của kỳ thi
    /// </summary>
    public enum TestStatus
    {
        Upcoming, //Chưa bắt đầu        
        Ongoing,    //Đang diễn ra        
        Finished,  //Đã kết thúc
        Creating, //Đang tạo
        InProgress, //Đang tiến hành
        Canceled   //Đã bị hủy
    }
}
