using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Entities
{
    [Table("Submissions")]
    public class Submission : BaseEntity
    {
        private DateTime SubmitDateTime;
        private string StudentId { get; set; }
        [ForeignKey("StudentId")]
        private Student Student { get; set; }
        private Guid ExamId { get; set; }
        private Exam Exam { get; set; }
        private double Grade { get; set; }
    }
}
