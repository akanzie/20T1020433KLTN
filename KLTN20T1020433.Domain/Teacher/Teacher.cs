
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Teacher
{
    /// <summary>
    /// Giảng viên
    /// </summary>
    public class Teacher
    {
        public string TeacherId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; } = "";

    }
}
