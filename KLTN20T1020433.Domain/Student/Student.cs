using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KLTN20T1020433.Domain.Student
{
    /// <summary>
    /// Sinh viên
    /// </summary>
    public class Student
    {

        public string StudentId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";

    }
}
