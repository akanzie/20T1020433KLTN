using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Entities
{
    /// <summary>
    /// Sinh viên - Lớp học phần
    /// </summary>
    public class StudentCourse
    {
        /// <summary>
        /// Mã sinh viên
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// Mã lớp học phần
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// Đã thanh toán học phí chưa
        /// </summary>
        public bool isTuitionPaid { get; set; }

    }
}
