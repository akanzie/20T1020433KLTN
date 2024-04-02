using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModelsModels.Entities;

namespace KLTN20T1020433.Web.Models
{
    public class SubmissionModel
    {
        public Submission Submission { get; set; }
        public List<SubmissionFile> Files { get; set; }
        public Comment Comment { get; set; }
    }
}
