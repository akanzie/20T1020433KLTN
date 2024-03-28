using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Enum;
using _20T1020433KLTN.Domain.Interfaces;
using Microsoft.VisualBasic;
using Nhom2.DataLayers.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.BussinessLayers
{
    public static class StudentService
    {
        private static readonly ITestDAL testDB;
        private static readonly ISubmissionDAL submissionDB;
        static StudentService()
        {
            string connectionString = Configuration.ConnectionString;

            testDB = new TestDAL(connectionString);
            submissionDB = new SubmissionDAL(connectionString);

        }
        public static List<Test> GetTestsForStudentHome(int page = 1, int pageSize = 0,
            string studentId = "")
        {
            return testDB.GetTestsForStudentHome(page, pageSize, studentId).ToList();
        }
        public static List<Test> GetTestsByStudent(int page = 1, int pageSize = 0,
            string studentId = "", string searchValue = "", TestType testType = TestType.All,
            TestStatus testStatus = TestStatus.All, DateTime? fromTime = null, DateTime? toTime = null)
        {
            return testDB.GetTestsByStudent(page, pageSize, studentId, searchValue, testType, testStatus, fromTime, toTime).ToList();
        }
        public static Test? GetTest(long testId)
        {
            return testDB.GetById(testId);
        }
        public static int SubmitTest(string studentId, long testId, IEnumerable<SubmissionFile> files, string iPAddress, DateTime submitTime)
        {
            if (files.Count() == 0)
            {
                return 0;
            }
            SubmissionStatus status = SubmissionStatus.Submitted;
            Test test = testDB.GetById(testId);
            if (Utils.CheckIPAddress(iPAddress))
            {
                status = SubmissionStatus.PendingProcessing;
            }
            else if (submitTime > test.EndTime)
            {
                status = SubmissionStatus.LateSubmission;
            }

            Submission data = new Submission()
            {
                StudentId = studentId,
                TestId = testId,
                IPAddress = iPAddress,
                SubmitTime = submitTime,
                Status = status
            };

            int submissionId = submissionDB.Add(data);

            if (submissionId > 0)
            {
                foreach (var item in files)
                {
                    submissionDB.AddFileBySubmission(item);

                }
                return submissionId;
            }

            return 0;
        }
        public static bool CancelSubmission(long submissionId)
        {
            Submission? submission = submissionDB.Get(submissionId);
            Test? test = testDB.GetById(submission.TestId);
            if (submission == null)
                return false;
            if (test == null)
                return false;
            if (submission.Status == SubmissionStatus.Submitted && test.Status == TestStatus.Ongoing)
            {
                submission.Status = SubmissionStatus.NotSubmitted;
                data.FinishedTime = DateTime.Now;

                return orderDB.Update(data);
            }

            return false;
        }
        public static bool EditSubmission(long submissionID, string studentId, long testId, IEnumerable<SubmissionFile> files, string iPAddress, DateTime submitTime)
        {
            if (files.Count() == 0)
            {
                return false;
            }
            SubmissionStatus status = SubmissionStatus.Submitted;
            Test test = testDB.GetById(testId);
            if (Utils.CheckIPAddress(iPAddress))
            {
                status = SubmissionStatus.PendingProcessing;
            }
            else if (submitTime > test.EndTime)
            {
                status = SubmissionStatus.LateSubmission;
            }

            Submission newSubmission = new Submission()
            {
                SubmissionId = submissionID,
                StudentId = studentId,
                TestId = testId,
                IPAddress = iPAddress,
                SubmitTime = submitTime,
                Status = status
            };
     
            foreach (var item in files)
            {
                submissionDB.DeleteFileBySubmission();
            }

            if (submissionDB.Update(data))
            {
                foreach (var item in files)
                {
                    submissionDB.AddFileBySubmission(item);
                }

                return true;
            }

            return false;
        }
    }
}
