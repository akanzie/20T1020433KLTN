using _20T1020433KLTN.DataLayers.SQLServer;
using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Enum;
using _20T1020433KLTN.Domain.Interfaces;
using _20T1020433KLTN.Infrastructure.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.BussinessLayers
{
    public static class TeacherService
    {
        private static readonly ITestDAL testDB;
        private static readonly ISubmissionDAL submissionDB;
        private static readonly ICourseDAL courseDB;
        static TeacherService()
        {
            string connectionString = Configuration.ConnectionString;

            testDB = new TestDAL(connectionString);
            submissionDB = new SubmissionDAL(connectionString);

        }
        public static List<Test> GetTestsOfTeacher(int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType testType = TestType.All,
           TestStatus testStatus = TestStatus.All, DateTime? fromTime = null, DateTime? toTime = null)
        {
            return testDB.GetTestsOfTeacher(page, pageSize, teacherId, searchValue, testType, testStatus, fromTime, toTime).ToList();

        }
        public static List<Course> GetCourses(string teacherId)
        {
            return courseDB.GetCourses(teacherId).ToList();
        }
        public static List<Student> GetStudentsOfCourse(string courseId)
        {
            return courseDB.GetStudentsByCourse(courseId).ToList();
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
            return testDB.DeleteTest(testId);
        }
    }
}
