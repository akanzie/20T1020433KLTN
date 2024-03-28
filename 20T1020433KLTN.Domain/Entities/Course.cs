using _20T1020433KLTN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Entities
{
    /// <summary>
    /// Lớp học phần
    /// </summary>
    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; } = "";
        public string TeacherId { get; set; }

    }
}
