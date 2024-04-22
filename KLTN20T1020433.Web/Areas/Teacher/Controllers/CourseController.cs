using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Controllers.Teacher
{
    [Area("Teacher")]
    [Authorize]
    public class CourseController : Controller
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
