
using System.IO;
using System.IO.Compression;
using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
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
        public static MemoryStream ZipFiles(IEnumerable<GetFileResponse> files)
        {
            MemoryStream zipStream = new MemoryStream();

            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var item in files)
                {
                    if (File.Exists(item.FilePath))
                    {                        
                        byte[] fileBytes = File.ReadAllBytes(item.FilePath);
                        ZipArchiveEntry entry = archive.CreateEntry(item.OriginalName);
                        using var entryStream = entry.Open();
                        using var pdfFileStream = System.IO.File.OpenRead(item.FilePath);
                        pdfFileStream.CopyTo(entryStream);
                    }
                }
            }
            zipStream.Seek(0, SeekOrigin.Begin);

            return zipStream;
        }        
    }
}
