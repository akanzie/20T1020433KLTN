using AutoMapper;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Course;
using KLTN20T1020433.Domain.Student;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KLTN20T1020433.Web.Configuration
{
    public static class CustomizeDatabase
    {
        public static void AddCustomizeDatabase(this IServiceCollection services)
        {            
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ITestFileRepository, TestFileRepository>();
            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            services.AddScoped<ISubmissionFileRepository, SubmissionFileRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
        }
    }
}
