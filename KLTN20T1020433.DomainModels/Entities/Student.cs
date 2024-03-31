using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN20T102433.Infrastructure.Entities;

namespace KLTN20T102433.Domain.Entities
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
