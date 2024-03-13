using _20T1020433KLTN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Interfaces
{
    public interface ITeacherService
    {
        int CreateExam(Exam exam, string teacherId);
        int EditExam(Exam exam, string teacherId);
        int DeleteExam(long examId);
        bool GradeSubmission(Guid submissionId, string studentId, double score);
    }
}
