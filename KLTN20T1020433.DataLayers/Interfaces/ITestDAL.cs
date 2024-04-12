
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
        Task<IList<Test>> GetTestsForStudentHome(int page = 1, int pageSize = 0,
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
        Task<IList<Test>> GetTestsOfStudent(int page = 1, int pageSize = 0,
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
        Task<IList<Test>> GetTestsOfTeacher(int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        Task<int> CountTestsOfTeacher(string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        Task<int> CountTestsOfStudent(string studentId = "", string searchValue = "", TestType? testType = null,
          TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        Task<int> CountTestsForStudentHome(string studentId = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        Task<Test?> GetById(int testId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> Add(Test data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<bool> Update(Test data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        Task<bool> Delete(int testID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<Guid> AddTestFile(TestFile file);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<bool> DeleteTestFile(Guid fileId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        Task<IList<TestFile>> GetFilesOfTest(int testId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<TestFile?> GetTestFile(Guid fileId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        
        Task<bool> IsUsed(int id);
        Task<bool> CheckFileOwner(string teacherId, Guid fileId);

    }
}
