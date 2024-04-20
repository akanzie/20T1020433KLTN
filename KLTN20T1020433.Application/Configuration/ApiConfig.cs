using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Configuration
{
    public class ApiConfig
    {
        public const string API_CONFIG = "ApiConfig";
        public string Host { get; set; } = String.Empty;
        public string AppId { get; set; } = String.Empty;
        public string SecretKey { get; set; } = String.Empty;
    }
}
