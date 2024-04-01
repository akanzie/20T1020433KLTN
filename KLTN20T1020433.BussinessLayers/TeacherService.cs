using DataLayers.Interfaces;
using KLTN20T1020433.DataLayers.API;
using KLTN20T1020433.DataLayers.Interfaces;
using KLTN20T1020433.DataLayers.SQLServer;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using KLTN20T1020433.DomainModels.Interfaces;
using KLTN20T1020433.Infrastructure.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.BusinessLayers
{
    public static class TeacherService
    {
        private static readonly ITestDAL testDB;
        private static readonly ISubmissionDAL submissionDB;
        private static readonly ICourseDAL courseDB;
        private static readonly IStudentDAL studentDB;
        static TeacherService()
        {
            string connectionString = Configuration.ConnectionString;

            testDB = new TestDAL(connectionString);
            submissionDB = new SubmissionDAL(connectionString);

        }
        public static List<Test> GetTestsOfTeacher(out int rowCount, int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            rowCount = testDB.CountTestsOfTeacher(searchValue);
            return testDB.GetTestsOfTeacher(page, pageSize, teacherId, searchValue, testType, testStatus, fromTime, toTime).ToList();

        }
        public static async Task<List<Course>> GetCourses(string teacherId)
        {
            try
            {
                return await courseDB.GetCourses(teacherId);
            }
            catch (Exception ex)
            {
                // Xử lý bất kỳ ngoại lệ nào xảy ra, ghi nhật ký hoặc ném lại nếu cần
                Console.WriteLine($"Đã xảy ra lỗi khi lấy danh sách khóa học: {ex.Message}");
                throw;
            }
        }
        public static async Task<List<Student>> GetStudentsOfCourse(string courseId)
        {
            return await studentDB.GetStudentsOfCourse(courseId);
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
