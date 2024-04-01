using KLTN20T1020433.DomainModels.Entities.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Entities
{
    public class SubmissionDto 
    {
        private DateTime SubmitDateTime;
        public Guid StudentId { get; set; }
        public StudentDto Student { get; set; }
    }
}
