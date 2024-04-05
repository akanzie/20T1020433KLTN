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
        public static async Task<List<Test>> GetTestsOfTeacher(int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {

            return (await testDB.GetTestsOfTeacher(page, pageSize, teacherId, searchValue, testType, testStatus, fromTime, toTime)).ToList();

        }
        public static async Task<int> GetRowCount(string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            return await testDB.CountTestsOfTeacher(searchValue);
        }
        public static async Task<List<Course>> GetCourses(string teacherId)
        {
            try
            {
                return (await courseDB.GetCourses(teacherId)).ToList();
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
            return (await studentDB.GetStudentsOfCourse(courseId)).ToList();
        }

        public static async Task<Test?> GetTest(int testId)
        {
            return await testDB.GetById(testId);
        }

        public static async Task<Guid> UploadTestFile(int testId, TestFile file)
        {
            file.TestId = testId;
            return await testDB.AddTestFile(file);
        }

        public static async Task<List<TestFile>> GetFilesOfTest(int testId)
        {
            return (await testDB.GetFilesOfTest(testId)).ToList();
        }

        public static async Task<List<Submission>> GetSubmissionsOfTest(int testId)
        {
            return (await submissionDB.GetSubmissions(testId)).ToList();
        }

        public static async Task<int> CreateTest(string teacherId)
        {
            Test test = new Test()
            {
                TeacherId = teacherId,
                CreatedTime = DateTime.Now,
            };

            return await testDB.Add(test);
        }

        public static async Task<bool> DeleteTestFile(string teacherId, Guid fileId)
        {
            if (await testDB.CheckFileOwner(teacherId, fileId))
            {
                return await testDB.DeleteTestFile(fileId);
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> InitTest(Test test, IEnumerable<string> studentIds)
        {
            bool result = await testDB.Update(test);

            if (result)
            {
                if (studentIds.Count() > 0)
                    foreach (var studentId in studentIds)
                    {
                        await testDB.AddStudentParticipantTest(studentId, test.TestId);
                    }
                return true;
            }
            return false;
        }

        public static async Task<bool> EditTest(Test test)
        {
            return await testDB.Update(test);
        }

        public static async Task<bool> DeleteTest(int testId)
        {
            var data = await testDB.GetById(testId);
            if (data == null)
                return false;

            if (data.Status == TestStatus.Upcoming || data.Status == TestStatus.Canceled)
                return await testDB.Delete(testId);
            return false;
        }

    }
}
