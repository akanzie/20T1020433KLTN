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
        public static List<Test> GetTestsForStudentHome(out int rowCount, int page = 1, int pageSize = 0,
            string studentId = "")
        {
            rowCount = testDB.CountTestsForStudentHome(studentId);
            return testDB.GetTestsForStudentHome(page, pageSize, studentId).ToList();
        }
        public static List<Test> GetTestsByStudent(out int rowCount, int page = 1, int pageSize = 0,
            string studentId = "", string searchValue = "", TestType? testType = null,
            TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            rowCount = testDB.CountTestsOfStudent(studentId, searchValue, testType, testStatus, fromTime, toTime);
            return testDB.GetTestsOfStudent(page, pageSize, studentId, searchValue, testType, testStatus, fromTime, toTime).ToList();
        }
        public static Test? GetTest(int testId)
        {
            return testDB.GetById(testId);
        }
        public static bool SubmitTest(IPAddress ipAddress, int submissionId = 0)
        {
            if (submissionDB.GetFilesOfSubmission(submissionId).Count == 0)
            {
                return false;
            }
            Submission? submission = submissionDB.GetById(submissionId);
            submission.SubmittedTime = DateTime.Now;
            Test? test = testDB.GetById(submission.TestId);
            if (submission.SubmittedTime > test.EndTime)
                submission.Status = SubmissionStatus.LateSubmission;
            else if (Utils.CheckIPAddress(ipAddress))
            {
                submission.Status = SubmissionStatus.PendingProcessing;
            }
            else
            {
                submission.Status = SubmissionStatus.Submitted;
            }
            bool result = submissionDB.Update(submission);

            return result;
        }
        public static bool CancelSubmission(int submissionId)
        {
            Submission? submission = submissionDB.GetById(submissionId);
            Test? test = testDB.GetById(submission.TestId);
            if (submission == null)
                return false;
            if (test == null)
                return false;
            if (submission.Status == SubmissionStatus.Submitted && test.Status == TestStatus.Ongoing)
            {
                submission.Status = SubmissionStatus.NotSubmitted;
                submission.SubmittedTime = DateTime.Now;

                return submissionDB.Update(submission);
            }

            return false;
        }        
        public static List<SubmissionFile> GetFilesOfSubmission(int submissionId)
        {
            Submission? submission = submissionDB.GetById(submissionId);
            if (submission == null)
            {
                return new List<SubmissionFile>();
            }
            List<SubmissionFile> files = submissionDB.GetFilesOfSubmission(submission.SubmissionId).ToList();
            if (files != null)
            {
                return files.ToList();
            }
            else
            {
                return new List<SubmissionFile>();
            }
        }

        public static Submission? GetSubmission(int id)
        {
            return submissionDB.GetById(id);
        }
        public static Submission? GetSubmission(int testId, string studentId)
        {
            return submissionDB.Get(testId, studentId);
        }
        public static List<Comment> GetComments(int submissionId)
        {
            List<Comment> comments = commentDB.GetComments(submissionId).ToList();
            return comments;
        }
    }
}
