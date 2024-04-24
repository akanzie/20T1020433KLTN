using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models;

namespace KLTN20T1020433.Web.Mappings
{
    public class AutoMapperConfig
    {
        public static List<Profile> RegisterMappings()
        {
            var cfg = new List<Profile>
            {
                // Thêm các MappingProfile khác vào đây
                new StudentMappingProfile(),
                new TeacherMappingProfile()
            };

            return cfg;
        }
        
    }
}
