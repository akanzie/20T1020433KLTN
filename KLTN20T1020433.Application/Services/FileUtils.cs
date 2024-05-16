using KLTN20T1020433.Application.DTOs;
using SautinSoft;
using System.Diagnostics;
using System.IO.Compression;
using static SautinSoft.RtfToHtml;


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
        public static string ConvertToHtmlAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            try
            {
                string outputHtmlFile = Path.ChangeExtension(filePath, ".html"); 
                RtfToHtml r = new RtfToHtml();
                r.Convert(filePath, outputHtmlFile, new HtmlFixedSaveOptions() { Title = "SautinSoft Example." });

                // Kiểm tra xem tệp HTML đầu ra đã được tạo thành công chưa
                if (!File.Exists(outputHtmlFile))
                {
                    throw new Exception("Failed to convert Word file to HTML.");
                }

                // Đọc nội dung HTML từ tệp HTML đầu ra
                string html = File.ReadAllText(outputHtmlFile);

                // Trả về chuỗi HTML
                return html;
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting Word file to HTML.", ex);
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
