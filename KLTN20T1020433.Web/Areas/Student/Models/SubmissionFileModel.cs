﻿using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Domain.Submission;

namespace KLTN20T1020433.Web.Areas.Student.Models
{
    public class SubmissionFileModel
    {
        public SubmissionStatus Status { get; set; }
        public IEnumerable<GetSubmissionFileResponse> Files { get; set; }
    }
}