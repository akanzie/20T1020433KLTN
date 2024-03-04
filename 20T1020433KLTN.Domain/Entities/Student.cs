using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Infrastructure.Entities;

namespace _20T1020433KLTN.Domain.Entities
{
    [Table("Students")]
    public class Student : BaseEntity
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClass> CourseClasses { get; set; }

    }
}
