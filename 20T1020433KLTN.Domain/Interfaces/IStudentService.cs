using _20T1020433KLTN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Interfaces
{
    public interface IStudentService
    {
        List<Exam> GetPendingExams(string studentId);
        void SubmitExam(string studentId, Guid examId, Submission submissionDetails);
        void UpdateSubmission(string studentId, Guid examId, Submission newSubmissionDetails);
        void CancelSubmission(string studentId, Guid examId);
    }
}
