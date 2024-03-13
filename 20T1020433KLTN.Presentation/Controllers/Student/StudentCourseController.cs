using Microsoft.AspNetCore.Mvc;

namespace _20T1020433KLTN.Application.Controllers.Student
{
    public class StudentCourseController : Controller
    {
        public IActionResult Detail(int i = 0)
        {
            return View();
        }
        public IActionResult ListExam(int i = 0)
        {
            return View();
        }
    }
}
