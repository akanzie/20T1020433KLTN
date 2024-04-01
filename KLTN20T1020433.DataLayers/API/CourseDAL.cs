using DataLayers.Interfaces;
using KLTN20T1020433.DataLayers.Interfaces;
using KLTN20T1020433.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.API
{
    public class CourseDAL :  _BaseApi, ICourseDAL
    {
        public CourseDAL(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<List<Course>> GetCourses(string teacherId)
        {
            try
            {
                // Make a GET request to the API endpoint to retrieve courses for the given teacherId
                string endpoint = $"/api/courses?teacherId={teacherId}";
                List<Course> courses = await GetAsync<List<Course>>(endpoint);
                return courses;
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log or rethrow as needed
                Console.WriteLine($"Error occurred while getting courses: {ex.Message}");
                throw;
            }
        }
    }
}
