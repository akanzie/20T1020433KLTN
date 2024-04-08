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
    public static class FileDataService
    {
        private static readonly ISubmissionDAL submissionDB;
        private static readonly ITestDAL testDB;
        static FileDataService()
        {
            string connectionString = Configuration.ConnectionString;

            submissionDB = new SubmissionDAL(connectionString);
            testDB = new TestDAL(connectionString);
        }
        public static async Task<Guid> AddSubmissionFile(SubmissionFile file)
        {
            return await submissionDB.AddSubmissionFile(file);
        }

        public static async Task<Guid> AddTestFile(TestFile file)
        {
            return await testDB.AddTestFile(file);
        }

        public static async Task<SubmissionFile?> GetSubmissionFile(Guid id)
        {
            return await submissionDB.GetSubmissionFile(id);
        }

        public static async Task<bool> RemoveSubmissionFile(Guid id)
        {
            return await submissionDB.DeleteSubmissionFile(id);
        }
        public static async Task<bool> CheckFileAuthorize(string studentId, Guid id)
        {
            return await submissionDB.CheckFileAuthorize(studentId, id);
        }
        public static async Task<List<SubmissionFile>> GetFilesOfSubmission(int submissionId)
        {
            Submission? submission = await submissionDB.GetById(submissionId);
            if (submission == null)
            {
                return new List<SubmissionFile>();
            }
            List<SubmissionFile> files = (await submissionDB.GetFilesOfSubmission(submission.SubmissionId)).ToList();
            if (files != null)
            {
                return files.ToList();
            }
            else
            {
                return new List<SubmissionFile>();
            }
        }
        public static async Task<Guid> UploadTestFile(int testId, TestFile file)
        {
            file.TestId = testId;
            return await testDB.AddTestFile(file);
        }

        public static async Task<List<TestFile>> GetFilesOfTest(int testId)
        {
            Test? test = await testDB.GetById(testId);
            if (test == null)
            {
                return new List<TestFile>();
            }
            List<TestFile> files = (await testDB.GetFilesOfTest(test.TestId)).ToList();
            if (files != null)
            {
                return files.ToList();
            }
            else
            {
                return new List<TestFile>();
            }
        }

        public static async Task<TestFile?> GetTestFile(Guid id)
        {
            return await testDB.GetTestFile(id);
        }

        public static async Task<bool> RemoveTestFile(Guid id)
        {
            return await testDB.DeleteTestFile(id);
        }
    }
}
