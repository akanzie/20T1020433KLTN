using _20T1020433KLTN.Application.Models;
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

        public IActionResult Index()
        {
            return View();
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
