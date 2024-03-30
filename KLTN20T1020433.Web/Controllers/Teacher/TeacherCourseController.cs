using Microsoft.AspNetCore.Mvc;

namespace _20T1020433KLTN.Application.Controllers.Teacher
{
    public class TeacherCourseController : Controller
    {
        public IActionResult Detail(int i = 0)
        {
            return View();
        }
        public IActionResult ListExam()
        {
            return View();
        }
        public IActionResult ListStudent()
        {
            return View();
        }
    }
}
