using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Submission
{
    public class SubmissionHistory
    {
        public int SubmissionId { get; set; }
        public DateTime? SubmitTime { get; set; } = null;
        public string IPAddress { get; set; }
    }
}
