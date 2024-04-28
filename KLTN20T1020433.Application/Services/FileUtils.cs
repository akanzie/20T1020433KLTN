
using System.IO;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using Microsoft.AspNetCore.Http;
using Xceed.Words.NET;


namespace KLTN20T1020433.Application.Services
{
    public static class FileUtils
    {
        public const long MAX_FILE_SIZE = 25 * 1024 * 1024;
        public static async Task<byte[]> ReadFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                throw new ArgumentException("Invalid file path.");
            }

            // Đọc dữ liệu từ tệp và trả về dưới dạng mảng byte
            return await System.IO.File.ReadAllBytesAsync(filePath);
        }
        public static string ReadDocxFile(string filePath)
        {
            try
            {
                // Mở tệp Word
                using (DocX document = DocX.Load(filePath))
                {
                    // Đọc nội dung của tệp Word và trả về dưới dạng chuỗi
                    return document.Text;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return "Error: " + ex.Message;
            }
        }
        


    }
}
