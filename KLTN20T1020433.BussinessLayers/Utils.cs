using _20T1020433KLTN.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.BussinessLayers
{
    public static class Utils
    {
        public static bool CheckIPAddress(string ipAddress)
        {
            if (ipAddress == null) return false;
            else if (ipAddress.Length == 0)
                return false;
            return true;
        }
        public static string GetTestStatusDisplayName(TestStatus status)
        {
            switch (status)
            {
                case TestStatus.All:
                    return "Tất cả";
                case TestStatus.Upcoming:
                    return "Chưa bắt đầu";
                case TestStatus.Ongoing:
                    return "Đang diễn ra";
                case TestStatus.Finished:
                    return "Đã kết thúc";
                case TestStatus.Cancelled:
                    return "Đã bị hủy";
                default:
                    return "";
            }
        }
    }
}
