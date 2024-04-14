using KLTN20T1020433.DataLayers.API;
using KLTN20T1020433.Domain.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class CourseRepository : _BaseApi, ICourseRepository
    {
        public CourseRepository(string baseUrl) : base(baseUrl)
        {
        }

        public Task<IEnumerable<Course>> GetCourseByTeacherId(string teacherId)
        {
            throw new NotImplementedException();
        }
    }
}
