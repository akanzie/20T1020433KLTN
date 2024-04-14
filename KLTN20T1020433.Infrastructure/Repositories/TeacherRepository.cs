using KLTN20T1020433.DataLayers.API;
using KLTN20T1020433.Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class TeacherRepository : _BaseApi, ITeacherRepository
    {
        public TeacherRepository(string baseUrl) : base(baseUrl)
        {
        }

        public Task<Teacher> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
