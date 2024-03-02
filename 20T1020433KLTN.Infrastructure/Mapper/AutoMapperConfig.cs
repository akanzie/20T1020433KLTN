using _20T1020433KLTN.Infrastructure.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Infrastructure.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<StudentDto, StudentDto>(); // Ánh xạ từ Student sang StudentDto
            CreateMap<StudentDto, StudentDto>(); // Ánh xạ từ StudentDto sang Student

            CreateMap<CourseClassDto, CourseClassDto>(); // Ánh xạ từ Course sang CourseDto
            CreateMap<CourseClassDto, CourseClassDto>(); // Ánh xạ từ CourseDto sang Course
        }
    }
}
