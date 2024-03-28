using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Infrastructure.Entities;

namespace _20T1020433KLTN.Domain.Entities
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
