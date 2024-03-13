﻿using System;
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
        /// <summary>
        /// Mã sinh viên
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// Họ tên sinh viên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Email của sinh viên
        /// </summary>
        public string Email { get; set; }

    }
}
