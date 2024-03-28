using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Enum;
using _20T1020433KLTN.Domain.Interfaces;
using _20T1020433KLTN.Infrastructure.Entities;
using Microsoft.VisualBasic;
using Nhom2.DataLayers.SQLServer;
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
        public static List<Test> GetTestsByTeacher(int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType testType = TestType.All,
           TestStatus testStatus = TestStatus.All, DateTime? fromTime = null, DateTime? toTime = null)
        {
            return testDB.GetTestsByTeacher(page, pageSize, teacherId, searchValue, testType, testStatus, fromTime, toTime).ToList();
        }
        public static List<Course> GetCourses(string teacherId)
        {
            return courseDB.GetCourses(teacherId).ToList();
        }
        public static List<Student> GetStudentsOfCourse(string courseId)
        {
            return courseDB.GetStudentsByCourse(courseId).ToList();
        }
        public static Test GetTest(long testId)
        {
            return testDB.GetById(testId);
        }
        public static int CreateTest(IEnumerable<TestFile> files, Test test, IEnumerable<string> studentIds)
        {
            int testId = testDB.Add(test);

            if (testId > 0)
            {
                if (files.Count() > 0)
                    foreach (var item in files)
                    {
                        testDB.AddTestFile(item);
                    }
                foreach (var studentId in studentIds)
                {
                    testDB.AddStudentParticipantTest(studentId, testId);
                }
                return testId;
            }

            return 0;
        }
        public static int EditTest(IEnumerable<TestFile> files, Test test, IEnumerable<string> studentIds)
        {
            int testId = testDB.Add(test);

            if (testId > 0)
            {
                if (files.Count() > 0)
                    foreach (var item in files)
                    {
                        testDB.AddTestFile(item);
                    }
                foreach (var studentId in studentIds)
                {
                    testDB.AddStudentParticipantTest(studentId, testId);
                }
                return testId;
            }

            return 0;
        }
        public static bool DeleteTest(long examId)
        {
            return testDB.Delete(examId);
        }
    }
}
