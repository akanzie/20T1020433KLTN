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
        Submission Get(long SubmissionId);
        IList<SubmissionFile> GetFilesBySubmission(string studentId = "", long submissionId = 0);

        IList<SubmissionFile> GetSubmissionFiles(string studentId = "");
        int Add(Submission data);

        bool Update(Submission data);

        bool Delete(long submissionId);

        int AddFileBySubmission(SubmissionFile file);

        bool DeleteFileBySubmission(SubmissionFile file);

        SubmissionFile? GetFile(int submission, int fileId);
    }
}
