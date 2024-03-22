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
        public IActionResult CreateExam()
        {
            return View();
        }
        public IActionResult CreateTest()
        {
            return View();
        }
        public IActionResult ListExam()
        {
            return View();
        }
        public IActionResult SelectStudents()
        {
            return View();
        }
        public IActionResult SelectCourses()
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
