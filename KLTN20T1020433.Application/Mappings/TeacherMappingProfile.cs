﻿using AutoMapper;
using KLTN20T1020433.Application.Commands.TeacherCommands.Create;
using KLTN20T1020433.Application.Commands.TeacherCommands.Update;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Student;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;


namespace KLTN20T1020433.Application.Mappings
{
    public class TeacherMappingProfile : Profile
    {

        public TeacherMappingProfile()
        {
            // Đưa hết các cấu hình bạn muốn map giữa các object vào đây
            // Thuộc tính FullName trong UserViewModel được kết hợp từ FirstName và LastName trong User
            CreateMap<CreateTestCommand, Test>();
            CreateMap<CreateTestCommand, UpdateTestCommand>();
            CreateMap<Test, GetTestByIdResponse>();
            CreateMap<GetTestByIdResponse, UpdateTestCommand>();
            CreateMap<Comment, GetCommentResponse>();
            CreateMap<Submission, GetSubmissionResponse>();
            CreateMap<Submission, GetSubmissionBySearchResponse>();
            CreateMap<SubmissionFile, GetSubmissionFileResponse>();
            CreateMap<TestFile, GetTestFileResponse>();
            CreateMap<Student, GetStudentResponse>();
            CreateMap<Test, GetTestBySearchResponse>()
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.HasValue ? src.EndTime.Value.ToString(Converter.TimeWithDateAndMonth) : ""))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.ToString(Converter.DateWithMonth)))
                .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdateTime.HasValue ? src.LastUpdateTime.Value.ToString(Converter.DateWithMonth) : ""));

        }

    }
}
