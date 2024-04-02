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
        IList<Submission> GetSubmissions(int testId);
        Submission? Get(int testId, string studentId);
        Submission? GetById(int submisionId);
        IList<SubmissionFile> GetFilesOfSubmission(int submisionId);
        int Add(Submission data);
        bool Update(Submission data);
        Guid AddSubmissionFile(SubmissionFile file);
        bool DeleteSubmissionFile(Guid fileId);
        SubmissionFile? GetSubmissionFile( Guid fileId);
    }
}
