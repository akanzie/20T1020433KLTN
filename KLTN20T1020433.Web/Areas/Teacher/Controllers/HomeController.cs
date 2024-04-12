using KLTN20T1020433.Domain.Course;
using KLTN20T1020433.Web.Models;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace KLTN20T1020433.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string teacherId)
        {
            List<Course> courses = await TeacherService.GetCourses(teacherId);

            // Trả về danh sách các khóa học đó cho view
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
