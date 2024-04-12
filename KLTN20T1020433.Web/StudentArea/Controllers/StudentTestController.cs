using KLTN20T1020433.BusinessLayers;
using KLTN20T1020433.DataLayers.SQLServer;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Net;
using System.Net.WebSockets;
using System.Reflection;

namespace KLTN20T1020433.Web.Controllers.Student
{
    public class StudentTestController : Controller
    {
        const int PAGE_SIZE = 10;
        const string TEST_SEARCH = "test_search";

        public IActionResult ListTest()
        {
            Models.TestSearchInput? input = ApplicationContext.GetSessionData<TestSearchInput>(TEST_SEARCH);
            if (input == null)
            {
                input = new TestSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    FromTime = null,
                    ToTime = null,
                    SearchValue = "",
                    Status = null,
                    Type = null,
                    StudentId = "20T1020433"
                };

            }
            return View(input);
        }
        public async Task<IActionResult> Search(TestSearchInput input)
        {
            var data = await StudentService.GetTestsForStudentHome(input.Page, input.PageSize, input.StudentId ?? "20T1020433");
            int rowCount = await StudentService.GetRowCount(input.StudentId ?? "20T1020433");
            var model = new TestSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                FromTime = input.FromTime,
                ToTime = input.ToTime,
                SearchValue = input.SearchValue ?? "",
                Status = input.Status,
                Type = input.Type,
                Data = data
            };

            // Lưu lại vào session điều kiện tìm kiếm
            ApplicationContext.SetSessionData(TEST_SEARCH, input);

            return View(model);

        }
        public async Task<IActionResult> Detail(int id = 0)
        {
            var test = await StudentService.GetTest(id);
            if (test == null)
            {
                return RedirectToAction("Index", "StudentHome");
            }
            List<TestFile> files = await FileDataService.GetFilesOfTest(id);
            var model = new TestModel
            {
                Test = test,
                Files = files
            };
            return View(model);

        }
        public async Task<IActionResult> Submission(int testId = 0)
        {
            string studentId = "20T1020433";
            var submission = await StudentService.GetSubmission(testId, studentId);

            if (submission != null)
            {
                var comments = await StudentService.GetComments(submission.SubmissionId);

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
        public async Task<IActionResult> UploadSubmissionFile(List<IFormFile> files, int testId = 0, int submissionId = 0)
        {
            if (files == null || files.Count == 0)
                return Json("Không có tệp nào được gửi.");
            else
            {
                foreach (var item in files)
                {
                    SubmissionFile submissionFile = await FileUtils.SaveSubmissionFileAsync(item, testId, submissionId);
                    await FileDataService.AddSubmissionFile(submissionFile);
                }
            }
            return Json("Tải file lên thành công.");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSubmissionFile(Guid id)
        {
            SubmissionFile? file = await FileDataService.GetSubmissionFile(id);
            if (file == null)
                return Json("Không tìm thấy file");
            else
            {
                FileUtils.DeleteFile(file.FilePath);
                await FileDataService.RemoveSubmissionFile(id);
            }
            return Json("Xóa file thành công.");
        }
        [HttpPost]
        public async Task<IActionResult> Submit(int id)
        {
            var submission = await StudentService.GetSubmission(id);
            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                Test? test = await StudentService.GetTest(submission.TestId);
                if (test!.IsConductedAtSchool && !Utils.CheckIPAddress(ipAddress))
                {

                    return BadRequest("Địa chỉ IP không hợp lệ. Bạn không được phép nộp bài.");
                }
                if (await StudentService.SubmitTest(ipAddress, id))
                {
                    submission = await StudentService.GetSubmission(id);
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = await StudentService.GetComments(id),
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
        public async Task<IActionResult> Cancel(int id)
        {
            var submission = await StudentService.GetSubmission(id);

            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;

                if (await StudentService.Cancel(ipAddress, id))
                {
                    submission = await StudentService.GetSubmission(id);
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = await StudentService.GetComments(id),
                    };

                    return PartialView("Submission", model);
                }
            }
            return BadRequest("Có lỗi xảy ra.");
        }
        public async Task<IActionResult> Download(Guid id)
        {
            string studentId = "20T1020433";
            bool isAuthorized = await FileDataService.CheckFileAuthorize(studentId, id);
            if (isAuthorized)
            {
                var fileInfo = await FileDataService.GetSubmissionFile(id);

                if (fileInfo == null)
                {
                    return BadRequest();
                }
                string filePath = fileInfo.FilePath;
                string mimeType = fileInfo.MimeType;
                if (!System.IO.File.Exists(filePath))
                {
                    return Json("Không tìm thấy file");
                }
                byte[] fileBytes = await FileUtils.ReadFileAsync(filePath);
                return File(fileBytes, mimeType, fileInfo.OriginalName);
            }
            else
            {
                return Json("Bạn không có quyền truy cập file.");
            }
        }       
        public async Task<IActionResult> ListSubmissionFiles(int submissionId = 0)
        {
            var submission = await StudentService.GetSubmission(submissionId);
            if (submission != null)
            {
                var files = await FileDataService.GetFilesOfSubmission(submissionId);
                var model = new SubmissionFileModel
                {
                    Files = files,
                    Submission = submission
                };
                return PartialView(model);
            }
            return BadRequest("Có lỗi xảy ra.");
        }
    }
}
