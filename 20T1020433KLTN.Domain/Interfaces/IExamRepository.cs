using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Interfaces
{
    public interface IExamRepository
    {
        IList<Exam> List(ExamStatus status = ExamStatus.All, string studentId = "");
        IList<Exam> List(ExamStatus status = ExamStatus.All, long courseId = 0);        
        int Add(Exam data, IEnumerable<ExamFile> files);
        Exam Get(long ExamId);
        void Update(Exam exam);
        void Delete(Guid id);
    }
}
