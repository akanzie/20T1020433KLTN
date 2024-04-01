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
        IList<Submission> GetSubmissionsOfTest(int testId);
        Submission Get(int SubmissionId);
        IList<SubmissionFile> GetFilesOfSubmission(int submissionId = 0);
        int Add(Submission data);
        bool Update(Submission data);
        bool Delete(int submissionId);
        int AddSubmissionFile(SubmissionFile file);
        bool DeleteSubmissionFile(SubmissionFile file);
        SubmissionFile? GetSubmissionFile(int submission, int fileId);
    }
}
