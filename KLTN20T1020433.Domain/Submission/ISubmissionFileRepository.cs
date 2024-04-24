using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Submission
{
    public interface ISubmissionFileRepository
    {
        Task<IEnumerable<SubmissionFile>> GetFilesBySubmissionId(int submisionId);
        Task<bool> Add(SubmissionFile file);
        Task<bool> Delete(Guid fileId);
        Task<SubmissionFile?> GetById(Guid id);
        Task<bool> CheckFileAuthorize(string studentId, Guid id);
    }
}
