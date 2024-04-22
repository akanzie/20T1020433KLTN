using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.DTOs.StudentDTOs
{
    public class GetProfileResponse
    {
        public string PhanLoai { get; set; }
        public string MaSinhVien { get; set; }
        public string MaGiangVien { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string NgaySinh { get; set; }
        public string Email { get; set; } 
    }
}
