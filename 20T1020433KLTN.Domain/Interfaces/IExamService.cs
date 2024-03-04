
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1020433KLTN.Domain.Entities;

namespace _20T1020433KLTN.Domain.Interfaces
{
    public interface IExamService
    {
        public Exam CreateExam(Exam examDetails);

        public Exam EditExam(Guid examId, Exam newExamDetails);

        public int DeleteExam(Guid examId);

        public List<Exam> GetAllExamsOfLecturer(string lecturerId);

        public Exam GetExamDetails(Guid examId);

    }
}
