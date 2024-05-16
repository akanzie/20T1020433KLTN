using KLTN20T1020433.Domain.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.DTOs.TeacherDTOs
{
    public class GetSubmissionBySearchResponse
    {
        public int SubmissionId { get; set; }
        public string StudentName { get; set; }
        public string SubmittedTime { get; set; }
        public string IpAddress { get; set; }
        public SubmissionStatus Status { get; set; }
        public string StatusDisplayName { get; set; }
        public int FilesCount { get; set; }
    }
}
