using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Entities
{
    /// <summary>
    /// Giảng viên
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Mã giảng viên
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// Họ tên của giảng viên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Email của giảng viên
        /// </summary>
        public string Email { get; set; }

    }
}
