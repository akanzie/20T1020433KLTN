using Microsoft.AspNetCore.Mvc;

namespace _20T1020433KLTN.Application.Controllers.Teacher
{
    public class TeacherExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateExam()
        {
            return View();
        }
    }
}
