using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.Mappings
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            // Đưa hết các cấu hình bạn muốn map giữa các object vào đây
            // Thuộc tính FullName trong UserViewModel được kết hợp từ FirstName và LastName trong User
            CreateMap<Test, GetTestByIdResponse>();
            CreateMap<Comment, GetCommentResponse>();
            CreateMap<Submission, GetSubmissionResponse>();
            CreateMap<SubmissionFile, GetSubmissionFileResponse>();
            CreateMap<TestFile, GetTestFileResponse>();
            // Trong hàm khởi tạo hoặc trong phương thức cấu hình AutoMapper của bạn
            CreateMap<Test, GetTestBySearchResponse>()
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.HasValue ? src.EndTime.Value.ToString(Converter.TimeWithDateAndMonth) : ""))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.ToString(Converter.TimeWithDateAndMonth) : ""));
        }
    }
}
