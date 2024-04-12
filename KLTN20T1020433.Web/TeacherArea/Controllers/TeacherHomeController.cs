using KLTN20T1020433.Web.Models;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KLTN20T1020433.BusinessLayers;

namespace KLTN20T1020433.Web.Controllers
{
    public class TeacherHomeController : Controller
    {
        private readonly ILogger<TeacherHomeController> _logger;

        public TeacherHomeController(ILogger<TeacherHomeController> logger)
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
