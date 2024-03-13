using Microsoft.AspNetCore.Mvc;

namespace _20T1020433KLTN.Application.Controllers.Student
{
    public class StudentExamController : Controller
    {
        public IActionResult Detail(int id = 0)
        {
            return View();
        }
        public IActionResult Submit()
        {
            return View();
        }
        public IActionResult Download()
        {
            return View();
        }
    }
}
