
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DomainModels.Interfaces
{
    public interface ITestDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        IList<Test> GetTestsForStudentHome(int page = 1, int pageSize = 0,
            string studentId = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="studentId"></param>
        /// <param name="searchValue"></param>
        /// <param name="testType"></param>
        /// <param name="testStatus"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        IList<Test> GetTestsOfStudent(int page = 1, int pageSize = 0,
           string studentId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="teacherId"></param>
        /// <param name="searchValue"></param>
        /// <param name="testType"></param>
        /// <param name="testStatus"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        IList<Test> GetTestsOfTeacher(int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        int CountTestsOfTeacher(string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        int CountTestsOfStudent(string studentId = "", string searchValue = "", TestType? testType = null,
          TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        int CountTestsForStudentHome(string studentId = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        Test? GetById(int testId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Test data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Test data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        bool Delete(int testID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Guid AddTestFile(TestFile file);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool DeleteTestFile(Guid fileId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        IList<TestFile> GetFilesOfTest(int testId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        TestFile? GetTestFile(Guid fileId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        bool AddStudentParticipantTest(string studentId, int testId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        bool DeleteStudentParticipantTest(string studentId, int testId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        IList<Student> GetStudentIdsParticipantTest(int testId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        bool IsUsed(int id);
        bool CheckFileOwner(string teacherId, Guid fileId);

    }
}
