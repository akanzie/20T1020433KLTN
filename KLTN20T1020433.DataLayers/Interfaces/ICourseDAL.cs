using KLTN20T1020433.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.Interfaces
{
    public interface ICourseDAL 
    {
        Task<List<Course>> GetCourses(string teacherId);

    }
}
