using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Interfaces
{
    public interface ISubmissionDAL
    {
        IList<Submission> GetSubmissionsOfTest(string testId);
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
