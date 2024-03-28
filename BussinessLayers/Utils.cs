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
    }
}
