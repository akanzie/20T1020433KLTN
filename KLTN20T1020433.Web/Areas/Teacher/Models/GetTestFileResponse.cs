namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class GetTestFileResponse
    {
        public Guid FileId { get; set; }
        public string OriginalName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string MimeType { get; set; }
        public long Size { get; set; }
    }
}
