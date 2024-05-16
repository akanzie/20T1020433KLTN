
namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class FileViewModel
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string MimeType { get; set; }
        public string? Content { get; set; }
        public bool IsImage { get; set; }
        public bool IsText { get; set; }
        public int TestId { get; set; }
    }
}
