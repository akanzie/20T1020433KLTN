using _20T1020433KLTN.Application.Models;
using _20T1020433KLTN.BussinessLayers;
using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _20T1020433KLTN.Application.Controllers
{
    public class TeacherHomeController : Controller
    {
        private readonly ILogger<TeacherHomeController> _logger;

        public TeacherHomeController(ILogger<TeacherHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string teacherId)
        {
            List<Course> courses = TeacherService.GetCourses(teacherId);

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
