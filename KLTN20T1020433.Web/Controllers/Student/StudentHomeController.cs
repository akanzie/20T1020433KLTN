using Microsoft.AspNetCore.Mvc;

namespace _20T1020433KLTN.Application.Controllers.Student
{
    public class StudentHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
