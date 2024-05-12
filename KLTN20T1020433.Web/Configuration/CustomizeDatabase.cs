using AutoMapper;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Student;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Configuration;

namespace KLTN20T1020433.Web.Configuration
{
    public static class CustomizeDatabase
    {
        public static void AddCustomizeDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ITestRepository>(provider =>
            { 
                return new TestRepository(connectionString); 
            });
            services.AddSingleton<ITestFileRepository>(provider =>
            {
                return new TestFileRepository(connectionString);
            });
            services.AddSingleton<ISubmissionRepository>(provider =>
            {
                return new SubmissionRepository(connectionString);
            });
            services.AddSingleton<ISubmissionFileRepository>(provider =>
            {
                return new SubmissionFileRepository(connectionString);
            });
            services.AddSingleton<ICommentRepository>(provider =>
            {
                return new CommentRepository(connectionString);
            });
            services.AddSingleton<ITeacherRepository>(provider =>
            {
                return new TeacherRepository(connectionString);
            });
            services.AddSingleton<IStudentRepository>(provider =>
            {
                return new StudentRepository(connectionString);
            });
        }        
    }
}
