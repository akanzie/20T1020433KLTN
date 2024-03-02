using _20T1020433KLTN.Domain.Entities.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    public class Submission : BaseEntity
    {
        private DateTime SubmitDateTime;
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
