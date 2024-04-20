using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.DTOs.StudentDTOs
{
    public class GetStudentProfileResponse
    {
        [JsonProperty("PhanLoai")]
        public string Role { get; set; }
        [JsonProperty("MaSinhVien")]
        public string StudentId { get; set; }
        [JsonProperty("Ho")]
        public string LastName { get; set; }
        [JsonProperty("Ten")]
        public string FirstName { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
