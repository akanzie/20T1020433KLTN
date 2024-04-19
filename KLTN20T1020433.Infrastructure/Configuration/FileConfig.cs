using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Configuration
{
    public class FileConfig 
    {
        public const string FILE_CONFIG = "FileConfig";
        public string FileStoragePath { get; set; } = String.Empty;
    }
}
