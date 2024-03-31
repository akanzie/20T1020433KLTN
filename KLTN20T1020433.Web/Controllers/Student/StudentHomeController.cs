using Microsoft.AspNetCore.Mvc;

namespace KLTN20T102433.Application.Controllers.Student
{
    public class StudentHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
