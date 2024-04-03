namespace KLTN20T1020433.Web.Models
{
    public class UploadSubmissionFileModel
    {
        public List<IFormFile> Files { get; set; }
        public int TestId { get; set; }
        public int StudentId { get; set; }
        public int SubmissionId { get; set; }
    }
}
