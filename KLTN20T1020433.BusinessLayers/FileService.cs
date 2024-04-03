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
        public static Guid AddSubmissionFile(SubmissionFile file)
        {
            return submissionDB.AddSubmissionFile(file);
        }

        public static SubmissionFile? GetSubmissionFile(Guid id)
        {
            return submissionDB.GetSubmissionFile(id);
        }

        public static bool RemoveSubmissionFile(Guid id)
        {
            return submissionDB.DeleteSubmissionFile(id);
        }
    }
}
