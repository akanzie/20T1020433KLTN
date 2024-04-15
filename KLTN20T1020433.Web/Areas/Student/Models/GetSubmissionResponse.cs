﻿using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Areas.Student.Models
{
    public class SubmissionModelResponse
    {
        public GetSubmissionResponse Submission { get; set; }
        public IEnumerable<GetCommentResponse> Comments { get; set; }
    }
    public class GetSubmissionResponse
    {
        public int SubmissionId { get; set; }
        public DateTime SubmittedTime { get; set; }
        public SubmissionStatus Status { get; set; }
        public string StatusDescription { get; set; }
        public int TestId { get; set; }
    }
}
