using KLTN20T1020433.DataLayers.SQLServer;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.BusinessLayers
{
    public static class FileService
    {
        private static readonly ISubmissionDAL submissionDB;
        static FileService()
        {
            string connectionString = Configuration.ConnectionString;
            
            submissionDB = new SubmissionDAL(connectionString);
        }
        public static async Task<Guid> AddSubmissionFile(SubmissionFile file)
        {
            return await submissionDB.AddSubmissionFile(file);
        }

        public static async Task<SubmissionFile?> GetSubmissionFile(Guid id)
        {
            return await submissionDB.GetSubmissionFile(id);
        }

        public static async Task<bool> RemoveSubmissionFile(Guid id)
        {
            return await submissionDB.DeleteSubmissionFile(id);
        }
    }
}
