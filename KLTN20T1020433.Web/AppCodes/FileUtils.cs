
using System.IO;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Configuration;
using Microsoft.AspNetCore.Http;


namespace KLTN20T1020433.Web.AppCodes
{
    public static class FileUtils
    {     
        public static async Task<byte[]> ReadFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                throw new ArgumentException("Invalid file path.");
            }

            // Đọc dữ liệu từ tệp và trả về dưới dạng mảng byte
            return await System.IO.File.ReadAllBytesAsync(filePath);
        }

        

        
    }
}
