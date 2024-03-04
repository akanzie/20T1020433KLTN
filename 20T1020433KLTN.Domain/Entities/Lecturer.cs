using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Entities
{
    [Table("Lecturers")]
    public class Lecturer : BaseEntity
    {
        private string FullName { get; set; }
        public string Email { get; set; }

        public ICollection<CourseClass> CourseClasses { get; set; }

    }
}
