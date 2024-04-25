using AutoMapper;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Web.Mappings
{
    public class TeacherMappingProfile : Profile
    {

        public TeacherMappingProfile()
        {
            // Đưa hết các cấu hình bạn muốn map giữa các object vào đây
            // Thuộc tính FullName trong UserViewModel được kết hợp từ FirstName và LastName trong User
            CreateMap<Test, GetTestByIdResponse>();
            CreateMap<Comment, GetCommentResponse>();
            CreateMap<Submission, GetSubmissionResponse>();
            CreateMap<Submission, GetSubmissionBySearchResponse>();
            CreateMap<SubmissionFile, GetSubmissionFileResponse>();
            CreateMap<TestFile, GetTestFileResponse>();
            CreateMap<Test, GetTestBySearchResponse>();
        }

    }
}
