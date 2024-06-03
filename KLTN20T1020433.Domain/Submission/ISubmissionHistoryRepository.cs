using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Submission
{
    public interface ISubmissionHistoryRepository
    {
        Task<int> Add(SubmissionHistory data);
        Task<bool> Delete(int id);
        Task<SubmissionHistory?> GetById(int id);
        Task<IEnumerable<SubmissionHistory>> GetHistorysBySubmissionId(int submissionId);
        Task<bool> CheckIPAddressExists(string iPAddress, int submissionId, int testId);
    }
}
