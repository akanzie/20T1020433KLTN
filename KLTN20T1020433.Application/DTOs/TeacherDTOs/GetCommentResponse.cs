namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{
    public class GetCommentResponse
    {
        public int CommentId { get; set; }
        public string Body { get; set; } = "";
        public string CommentedTime { get; set; } = "";
        public string TeacherName { get; set; } = "";
    }
}
