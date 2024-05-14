using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Models;

namespace KLTN20T1020433.Web.Areas.Teacher.Models
{
    public class SubmissonSearchResult : BasePaginationResult
    {
        public int TestId { get; set; } = 0;
        public string Statuses { get; set; }
        public IEnumerable<GetSubmissionBySearchResponse> Data { get; set; }
        public int SubmissionCount { get; set; }
    }
}
