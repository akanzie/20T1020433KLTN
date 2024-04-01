using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Controllers.Student
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
