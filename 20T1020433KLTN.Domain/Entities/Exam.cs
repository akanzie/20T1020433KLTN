using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Domain.Enum;
using _20T1020433KLTN.Infrastructure.Entities;

namespace _20T1020433KLTN.Domain.Entities
{
    /// <summary>
    /// Kỳ thi
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// Mã kỳ thi
        /// </summary>
        public long ExamId { get; set; }
        /// <summary>
        /// Tên kỳ thi
        /// </summary>
        public string ExamName { get; set; }
        /// <summary>
        /// Nội dung hoặc hướng dẫn
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Loại kỳ thi
        /// </summary>
        public ExamType ExamType { get; set; }
        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Mã lớp học phần 
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// Có kiểm tra địa chỉ IP không
        /// </summary>
        public bool IsCheckIP { get; set; }
        /// <summary>
        /// Có thi tại trường không
        /// </summary>
        public bool IsConductedAtSchool { get; set; }
        /// <summary>
        /// Thời gian tạo kỳ thi
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// Lần cập nhật cuối cùng
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public ExamStatus Status { get; set; }
    }
}
