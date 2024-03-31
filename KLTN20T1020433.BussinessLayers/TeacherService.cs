using DataLayers.Interfaces;
using KLTN20T102433.DataLayers.SQLServer;
using KLTN20T102433.Domain.Entities;
using KLTN20T102433.Domain.Enum;
using KLTN20T102433.Domain.Interfaces;
using KLTN20T102433.Infrastructure.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.BussinessLayers
{
    public static class TeacherService
    {
        private static readonly ITestDAL testDB;
        private static readonly ISubmissionDAL submissionDB;
        private static readonly ICommonDAL<Course> courseDB;
        static TeacherService()
        {
            string connectionString = Configuration.ConnectionString;

            testDB = new TestDAL(connectionString);
            submissionDB = new SubmissionDAL(connectionString);
            courseDB = new CourseDAL(connectionString);
        }
        public static List<Test> GetTestsOfTeacher(out int rowCount, int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            rowCount = testDB.Count(searchValue);
            return testDB.GetTestsOfTeacher(page, pageSize, teacherId, searchValue, testType, testStatus, fromTime, toTime).ToList();

        }
        public static List<Course> GetCourses(string teacherId)
        {
            return courseDB.GetCourses(teacherId).ToList();
        }
        public static List<Student> GetStudentsOfCourse(string courseId)
        {
            return courseDB.GetStudentsOfCourse(courseId).ToList();
        }
        public static Test GetTest(int testId)
        {
            return testDB.GetById(testId);
        }
        public static Guid UploadTestFile(int testId, TestFile file)
        {

            file.TestId = testId;
            return testDB.AddTestFile(file);
        }
        public static List<TestFile> GetFilesOfTest(int testId)
        {

            return testDB.GetFilesOfTest(testId).ToList();
        }
        public static List<Submission> GetSubmissionsOfTest(int testId)
        {

            return submissionDB.GetSubmissionsOfTest(testId).ToList();
        }

        public static int CreateTest(string teacherId)
        {
            Test test = new Test()
            {
                TeacherId = teacherId,
                CreatedTime = DateTime.Now,
            };

            return testDB.AddTest(test);
        }
        public static bool DeleteTestFile(string teacherId, Guid fileId)
        {
            if (testDB.CheckFileOwner(teacherId, fileId))
            {
                return testDB.DeleteTestFile(fileId);
            }
            else
            {
                return false;
            }
        }
        public static bool InitTest(Test test, IEnumerable<string> studentIds)
        {
            bool result = testDB.UpdateTest(test);

            if (result)
            {
                if (studentIds.Count() > 0)
                    foreach (var studentId in studentIds)
                    {
                        testDB.AddStudentParticipantTest(studentId, test.TestId);
                    }
                return true;
            }
            return false;
        }
        public static bool EditTest(Test test)
        {
            return testDB.UpdateTest(test);
        }
        public static bool DeleteTest(int testId)
        {
            var data = testDB.GetById(testId);
            if (data == null)
                return false;

            if (data.Status == TestStatus.Upcoming
            || data.Status == TestStatus.Canceled)
                return testDB.DeleteTest(testId);
            return false;
        }
    }
}
