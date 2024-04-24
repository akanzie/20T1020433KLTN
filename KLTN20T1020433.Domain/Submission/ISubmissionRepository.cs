using KLTN20T1020433.Domain.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Submission
{
    public interface ISubmissionRepository
    {
        Task<IEnumerable<Submission>> GetSubmissionsByTestId(int testId);
        Task<Submission?> GetByTestIdAndStudentId(int testId, string studentId);
        Task<Submission?> GetById(int id);
        Task<int> Add(Submission data);
        Task<bool> Update(Submission data);
        Task<bool> Delete(int id);
        Task<IEnumerable<Submission>> GetSubmissionsBySearch(int page = 1, int pageSize = 0, 
            int testId = 0, string searchValue = "", SubmissionStatus? status = null);
        Task<int> CountSubmissions(int testId = 0, string searchValue = "", SubmissionStatus? status = null);
    }
}
