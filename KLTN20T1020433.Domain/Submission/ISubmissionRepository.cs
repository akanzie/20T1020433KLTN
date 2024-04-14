using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Submission
{
    public interface ISubmissionRepository
    {
        Task<IEnumerable<Submission>> GetSubmissionByTestId(int testId);
        Task<Submission> GetByTestIdAndStudentId(int testId, string studentId);
        Task<Submission> GetById(int id);
        Task<int> Add(Submission data);
        Task<bool> Update(Submission data);
        Task<bool> Delete(int id);
    }
}
