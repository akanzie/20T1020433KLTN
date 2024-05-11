using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Test
{
    public interface ITestRepository
    {              
        Task<IEnumerable<Test>> GetTestsOfStudent(int page = 1, int pageSize = 0,
           string studentId = "", string searchValue = "", TestStatus? testStatus = null, TestType? testType = null,
            DateTime? fromTime = null, DateTime? toTime = null);
        Task<IEnumerable<Test>> GetTestsForStudentHome(int page = 1, int pageSize = 0,
           string studentId = "");
        Task<int> CountTestsForStudentHome(string studentId = "");
        Task<IEnumerable<Test>> GetTestsOfTeacher(int page = 1, int pageSize = 0,
           string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        Task<int> CountTestsOfTeacher(string teacherId = "", string searchValue = "", TestType? testType = null,
           TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null);
        Task<int> CountTestsOfStudent(string studentId = "", string searchValue = "", TestStatus? testStatus = null, TestType? testType = null,
           DateTime? fromTime = null, DateTime? toTime = null);       
        Task<Test?> GetById(int testId);       
        Task<int> Add(Test data);        
        Task<bool> Update(Test data);        
        Task<bool> Delete(int testId); 
        Task<bool> IsUsed(int id);
    }
}
