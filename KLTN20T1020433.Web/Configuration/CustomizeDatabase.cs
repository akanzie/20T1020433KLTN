using AutoMapper;
using KLTN20T1020433.Domain.Comment;

using KLTN20T1020433.Domain.Submission;

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
            services.AddScoped<ITestRepository>(provider =>
            { 
                return new TestRepository(connectionString); 
            });
            services.AddScoped<ITestFileRepository>(provider =>
            {
                return new TestFileRepository(connectionString);
            });
            services.AddScoped<ISubmissionRepository>(provider =>
            {
                return new SubmissionRepository(connectionString);
            });
            services.AddScoped<ISubmissionFileRepository>(provider =>
            {
                return new SubmissionFileRepository(connectionString);
            });
            services.AddScoped<ICommentRepository>(provider =>
            {
                return new CommentRepository(connectionString);
            });
        }        
    }
}
