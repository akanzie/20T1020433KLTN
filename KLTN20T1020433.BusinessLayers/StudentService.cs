using KLTN20T1020433.DataLayers.Interfaces;
using KLTN20T1020433.DataLayers.SQLServer;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using KLTN20T1020433.DomainModels.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.BusinessLayers
{
    public static class StudentService
    {
        private static readonly ITestDAL testDB;
        private static readonly ISubmissionDAL submissionDB;
        private static readonly ICommentDAL commentDB;
        private static readonly ITeacherDAL teacherDB;
        static StudentService()
        {
            string connectionString = Configuration.ConnectionString;

            testDB = new TestDAL(connectionString);
            submissionDB = new SubmissionDAL(connectionString);
            commentDB = new CommentDAL(connectionString);
        }
        public static async Task<List<Test>> GetTestsForStudentHome(int page = 1, int pageSize = 0,
            string studentId = "")
        {
            return (await testDB.GetTestsForStudentHome(page, pageSize, studentId)).ToList();
        }
        public static async Task<int> GetRowCount(string studentId)
        {
            return await testDB.CountTestsForStudentHome(studentId);
        }
        public static async Task<List<Test>> GetTestsByStudent(int page = 1, int pageSize = 0,
            string studentId = "", string searchValue = "", TestType? testType = null,
            TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            return (await testDB.GetTestsOfStudent(page, pageSize, studentId, searchValue, testType, testStatus, fromTime, toTime)).ToList();
        }
        public static async Task<int> GetRowCount(string studentId, string searchValue = "", TestType? testType = null,
            TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            return await testDB.CountTestsOfStudent(studentId, searchValue, testType, testStatus, fromTime, toTime);
        }
        public static async Task<Test?> GetTest(int testId)
        {
            return await testDB.GetById(testId);
        }
        public static async Task<bool> SubmitTest(IPAddress ipAddress, int submissionId = 0)
        {
            if ((await submissionDB.GetFilesOfSubmission(submissionId)).Count == 0)
            {
                return false;
            }
            Submission? submission = await submissionDB.GetById(submissionId);
            submission!.SubmittedTime = DateTime.Now;
            Test? test = await testDB.GetById(submission.TestId);
            if (submission.SubmittedTime > test!.EndTime)
                submission.Status = SubmissionStatus.LateSubmission;
            else
                submission.Status = SubmissionStatus.Submitted;
            if (test.IsCheckIP && Utils.CheckIpAddressExists(ipAddress))
            {
                submission.Status = SubmissionStatus.PendingProcessing;
            }
            if (test!.IsConductedAtSchool && !Utils.CheckIPAddress(ipAddress))
            {
                return false;
            }
            bool result = await submissionDB.Update(submission);
            return result;
        }
        public static async Task<bool> CancelSubmission(int submissionId)
        {
            Submission? submission = await submissionDB.GetById(submissionId);
            Test? test = await testDB.GetById(submission!.TestId);
            if (submission == null)
                return false;
            if (test == null)
                return false;
            if (submission.Status == SubmissionStatus.Submitted && test.Status == TestStatus.Ongoing)
            {
                submission.Status = SubmissionStatus.NotSubmitted;
                submission.SubmittedTime = DateTime.Now;

                return await submissionDB.Update(submission);
            }

            return false;
        }
        

        public static async Task<Submission?> GetSubmission(int id)
        {
            return await submissionDB.GetById(id);
        }
        public static async Task<Submission?> GetSubmission(int testId, string studentId)
        {
            return await submissionDB.Get(testId, studentId);
        }
        public static async Task<List<Comment>> GetComments(int submissionId)
        {
            List<Comment> comments = (await commentDB.GetComments(submissionId)).ToList();
            return comments;
        }

        public static async Task<bool> Cancel(IPAddress? ipAddress, int submissionId)
        {
            Submission? submission = await submissionDB.GetById(submissionId);

            submission!.Status = SubmissionStatus.NotSubmitted;

            bool result = await submissionDB.Update(submission);

            return result;
        }

            


    }
}
