using KLTN20T102433.DataLayers.SQLServer;
using KLTN20T102433.Domain.Entities;
using KLTN20T102433.Domain.Enum;
using KLTN20T102433.Domain.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.BussinessLayers
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
            string studentId = "", string searchValue = "", TestType? testType = null,
            TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            return testDB.GetTestsOfStudent(page, pageSize, studentId, searchValue, testType, testStatus, fromTime, toTime).ToList();
        }
        public static Test? GetTest(int testId)
        {
            return testDB.GetById(testId);
        }
        public static int SubmitTest(string studentId, int testId, IEnumerable<SubmissionFile> files, string iPAddress, DateTime submitTime)
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
                    submissionDB.AddSubmissionFile(item);

                }
                return submissionId;
            }

            return 0;
        }
        public static bool CancelSubmission(int submissionId)
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
                submission.SubmitTime = DateTime.Now;

                return submissionDB.Update(submission);
            }

            return false;
        }
        public static bool EditSubmission(int submissionID, string studentId, int testId, IEnumerable<SubmissionFile> files, string iPAddress, DateTime submitTime)
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
                submissionDB.DeleteSubmissionFile(item);
            }

            if (submissionDB.Update(newSubmission))
            {
                foreach (var item in files)
                {
                    submissionDB.AddSubmissionFile(item);
                }

                return true;
            }

            return false;
        }
    }
}
