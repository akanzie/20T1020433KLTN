using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Student
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentById(string id);
        Task<bool> Add(Student student);
    }
}
