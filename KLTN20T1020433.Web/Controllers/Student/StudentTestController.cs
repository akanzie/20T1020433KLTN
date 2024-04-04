using KLTN20T1020433.BusinessLayers;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;
using System.Reflection;

namespace KLTN20T1020433.Web.Controllers.Student
{
    public class StudentTestController : Controller
    {
        public IActionResult Detail(int id = 0)
        {
            var test = StudentService.GetTest(id);
            if (test == null)
            {
                return RedirectToAction("Index", "StudentHome");
            }
            List<TestFile> files = TeacherService.GetFilesOfTest(id);
            var model = new TestModel
            {
                Test = test,
                Files = files
            };
            return View(model);

        }
        public IActionResult Submission(int testId = 0)
        {
            string studentId = "20T1020433";
            var submission = StudentService.GetSubmission(testId, studentId);

            if (submission != null)
            {
                var comments = StudentService.GetComments(submission.SubmissionId);

                var model = new SubmissionModel
                {
                    Submission = submission,
                    Comments = comments
                };

                return PartialView(model);
            }
            else
            {
                return RedirectToAction("Index", "StudentHome");
            }
        }
        [HttpPost]
        public IActionResult UploadSubmissionFile(List<IFormFile> files, int testId = 0, int submissionId = 0)
        {
            if (files == null || files.Count == 0)
                return Json("Không có tệp nào được gửi.");
            else
            {
                foreach (var item in files)
                {
                    SubmissionFile submissionFile = FileUtils.SaveSubmissionFile(item, testId, submissionId);
                    FileService.AddSubmissionFile(submissionFile);
                }
            }
            return Json("Tải file lên thành công.");
        }
        [HttpPost]
        public IActionResult RemoveSubmissionFile(Guid id)
        {
            SubmissionFile? file = FileService.GetSubmissionFile(id);
            if (file == null)
                return Json("Không tìm thấy file");
            else
            {
                FileUtils.DeleteFile(file.FilePath);
                FileService.RemoveSubmissionFile(id);
            }
            return Json("Tải file lên thành công.");
        }
        [HttpPost]
        public IActionResult Submit(int id)
        {
            var submission = StudentService.GetSubmission(id);

            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;

                if (StudentService.SubmitTest(ipAddress, id))
                {
                    submission = StudentService.GetSubmission(id);
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = StudentService.GetComments(id),
                    };

                    return PartialView("Submission", model);
                }
                else
                {
                    return BadRequest("Bạn chưa gửi file.");
                }
            }
            return BadRequest("Có lỗi xảy ra.");
        }
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var submission = StudentService.GetSubmission(id);

            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;

                if (StudentService.Cancel(ipAddress, id))
                {
                    submission = StudentService.GetSubmission(id);
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = StudentService.GetComments(id),       
                    };

                    return PartialView("Submission",model);
                }                
            }
            return BadRequest("Có lỗi xảy ra.");
        }
        public IActionResult Download()
        {
            return View();
        }
        public IActionResult ListSubmissionFiles(int submissionId = 0)
        {
            var submission = StudentService.GetSubmission(submissionId);
            if (submission != null)
            {
                var files = StudentService.GetFilesOfSubmission(submissionId);
                var model = new SubmissionFileModel
                {
                    SubmissionFiles = files,
                    Submission = submission
                };
                return PartialView(model);
            }
            return BadRequest("Có lỗi xảy ra.");
        }
    }
}
