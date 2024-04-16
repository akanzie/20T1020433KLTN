using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Services
{
    public class Utils
    {
        public static bool CheckIPAddress(IPAddress ipAddress)
        {
            if (ipAddress == null) return false;
            else if (ipAddress.Address != 0)
                return false;
            return true;
        }
        public static bool CheckIPAddressExists(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
