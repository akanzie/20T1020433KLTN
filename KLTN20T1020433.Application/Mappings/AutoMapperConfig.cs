using AutoMapper;

namespace KLTN20T1020433.Application.Mappings
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
