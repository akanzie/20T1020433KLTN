using KLTN20T1020433.DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Entities
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
