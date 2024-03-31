using DataLayers.Interfaces;
using KLTN20T102433.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T102433.DataLayers.SQLServer
{
    public class CourseDAL : _BaseDAL, ICommonDAL<Course>
    {
        public CourseDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Course data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Course? Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsUsed(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Course> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public bool Update(Course data)
        {
            throw new NotImplementedException();
        }
    }
}
