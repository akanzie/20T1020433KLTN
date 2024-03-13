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
        IList<Exam> GetListExams(string studentId);
        IList<Exam> GetListExams(long courseId);
        Exam GetExamById(long examId);
        int SubmitExam(long examId, string studentId, string ipAddress, DateTime timeSubmit, List<SubmissionFile> submissionFiles);
        int EditSubmision(long examId, string studentId, string ipAddress, DateTime timeSubmit, List<SubmissionFile> submissionFiles);
        int DeleteSubmision(long submissionId, string studentId);
    }
}
