using KLTN20T1020433.DataLayers.API;
using KLTN20T1020433.Domain.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class StudentRepository : _BaseApi, IStudentRepository
    {
        public StudentRepository(string baseUrl) : base(baseUrl)
        {
        }

        public Task<Student> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudentByCourseId(string courseId)
        {
            throw new NotImplementedException();
        }
    }
}
