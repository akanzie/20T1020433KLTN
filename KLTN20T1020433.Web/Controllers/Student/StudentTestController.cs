using Microsoft.AspNetCore.Mvc;

namespace KLTN20T102433.Application.Controllers.Student
{
    public class StudentTestController : Controller
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
        public IActionResult ListExam()
        {
            return View();
        }
    }
}
