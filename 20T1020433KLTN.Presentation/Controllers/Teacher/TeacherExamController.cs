using Microsoft.AspNetCore.Mvc;

namespace _20T1020433KLTN.Application.Controllers.Teacher
{
    public class TeacherExamController : Controller
    {
        public IActionResult Detail(int examId = 0)
        {
            return View();
        }
        public IActionResult ListSubmission(int examId = 0)
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Submission(int submissionId = 0)
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Save()
        {
            return View();
        }
        public IActionResult Download()
        {
            return View();
        }
        public IActionResult Mark()
        {
            return View();
        }
    }
}
