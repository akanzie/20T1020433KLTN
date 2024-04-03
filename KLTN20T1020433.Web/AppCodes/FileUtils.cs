using KLTN20T1020433.BusinessLayers;
using System.IO;
using Microsoft.AspNetCore.Http;
using KLTN20T1020433.DomainModels.Entities;

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

        public static SubmissionFile SaveSubmissionFile(IFormFile file, int testId = 0, int submissionId = 0)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }
            Guid id = Guid.NewGuid();
            // Tạo một tên tệp duy nhất
            string uniqueFileName = $"{id}_{file.FileName}";
            // Tạo đường dẫn đầy đủ cho tệp

            string filePath = Path.Combine(Configuration.FileStoragePath, testId.ToString(), "Submission", uniqueFileName);

            // Lưu tệp vào đường dẫn được chỉ định
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            // Tạo đối tượng File từ thông tin của IFormFile
            return new SubmissionFile
            {
                FileId = id,
                FileName = file.FileName,
                FilePath = filePath,
                MimeType = file.ContentType,
                Size = file.Length,
                SubmissionId = submissionId,

            };
        }
    }
}
