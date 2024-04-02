using KLTN20T1020433.DataLayers.Interfaces;
using KLTN20T1020433.DataLayers.SQLServer;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using KLTN20T1020433.DomainModels.Interfaces;
using KLTN20T1020433.DomainModelsModels.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static int SubmitTest(string studentId, int testId, IEnumerable<SubmissionFile> files, string iPAddress, DateTime submittedTime)
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
            else if (submittedTime > test.EndTime)
            {
                status = SubmissionStatus.LateSubmission;
            }

            Submission data = new Submission()
            {
                StudentId = studentId,
                TestId = testId,
                IPAddress = iPAddress,
                SubmittedTime = submittedTime,
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
                SubmittedTime = submitTime,
                Status = status
            };

            foreach (var item in files)
            {
                submissionDB.DeleteSubmissionFile(item.FileId);
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
        public static List<SubmissionFile> GetFilesOfSubmission(int id, string studentId)
        {
            Submission submission = submissionDB.Get(id, studentId);
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

        public static Submission GetSubmissionOfStudent(int id, string studentId)
        {
            return submissionDB.Get(id, studentId);
        }

        public static Comment? GetComment(int submissionId)
        {
            Comment? comment = commentDB.GetBySubmissionId(submissionId);
            if(comment != null)
            {
                comment.TeacherName = teacherDB.GetTeacher(comment.TeacherId).FullName;
            }
            return comment;
        }
    }
}
