using KLTN20T102433.Domain.Entities.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.Infrastructure.Entities
{
    public class SubmissionDto 
    {
        private DateTime SubmitDateTime;
        public Guid StudentId { get; set; }
        public StudentDto Student { get; set; }
    }
}
