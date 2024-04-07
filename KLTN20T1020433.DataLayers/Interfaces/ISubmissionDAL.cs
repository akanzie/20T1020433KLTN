using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DomainModels.Interfaces
{
    public interface ISubmissionDAL
    {
        Task<IList<Submission>> GetSubmissions(int testId);
        Task<Submission?> Get(int testId, string studentId);
        Task<Submission?> GetById(int submisionId);
        Task<IList<SubmissionFile>> GetFilesOfSubmission(int submisionId);
        Task<int> Add(Submission data);
        Task<bool> Update(Submission data);
        Task<Guid> AddSubmissionFile(SubmissionFile file);
        Task<bool> DeleteSubmissionFile(Guid fileId);
        Task<SubmissionFile?> GetSubmissionFile( Guid fileId);
        Task<bool> CheckFileAuthorize(string studentId, Guid id);
    }
}
