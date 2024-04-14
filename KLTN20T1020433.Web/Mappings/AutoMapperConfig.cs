using AutoMapper;
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
                new MappingProfile()
            };

            return cfg;
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // Đưa hết các cấu hình bạn muốn map giữa các object vào đây
                // Thuộc tính FullName trong UserViewModel được kết hợp từ FirstName và LastName trong User
                CreateMap<Test, GetTestResponse>();
                CreateMap<TestFile, GetTestResponse>();
            }
        }
    }
}
