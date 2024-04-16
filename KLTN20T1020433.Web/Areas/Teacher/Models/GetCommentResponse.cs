namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class GetCommentResponse
    {
        public int CommentId { get; set; }
        public string Body { get; set; } = "";
        public DateTime CommentedTime { get; set; }
        public string TeacherName { get; set; } = "";
    }
}
