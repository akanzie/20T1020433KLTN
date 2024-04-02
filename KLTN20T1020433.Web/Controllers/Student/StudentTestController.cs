using KLTN20T1020433.BusinessLayers;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace KLTN20T1020433.Web.Controllers.Student
{
    public class StudentTestController : Controller
    {
        public IActionResult Detail(int id = 0)
        {
            var test = StudentService.GetTest(id);
            if (test == null)
            {
                return RedirectToAction("Index");
            }
            List<TestFile> files = TeacherService.GetFilesOfTest(id);
            var model = new TestModel
            {
                Test = test,
                Files = files
            };
            return View(model);

        }
        public IActionResult Submission(int id = 0)
        {
            string studentId = "20T1020433";
            var submission = StudentService.GetSubmissionOfStudent(id, studentId);

            // Kiểm tra xem submission có tồn tại không
            if (submission != null)
            {
                var files = StudentService.GetFilesOfSubmission(id, studentId);
                var comment = StudentService.GetComment(submission.SubmissionId);
                var model = new SubmissionModel
                {
                    Submission = submission,
                    Files = files,
                    Comment = comment
                };
                return PartialView(model);
            }
            else
            {
                // Trả về một view hoặc thông báo lỗi phù hợp nếu submission không tồn tại
                return NotFound(); // Ví dụ: Trả về lỗi 404 - Không tìm thấy
            }
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
