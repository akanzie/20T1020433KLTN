using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Student.Commands.Update;
using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Areas.Student.Queries.GetCommentsBySubmissionId;
using KLTN20T1020433.Web.Areas.Student.Queries.GetSubmission;
using KLTN20T1020433.Web.Areas.Student.Queries.GetSubmissionById;
using KLTN20T1020433.Web.Areas.Student.Queries.GetTest;
using KLTN20T1020433.Web.Areas.Student.Queries.GetTestFilesByTestId;
using KLTN20T1020433.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Net;
using System.Net.WebSockets;
using System.Reflection;

namespace KLTN20T1020433.Web.Controllers.Student
{
    public class TestController : Controller
    {
        const int PAGE_SIZE = 10;
        const string TEST_SEARCH = "test_search";
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
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
            var test = await _mediator.Send(new GetTestByIdQuery { Id = id });
            if (test == null)
            {
                return RedirectToAction("Index", "StudentHome");
            }
            IEnumerable<GetTestFileResponse> files = await _mediator.Send(new GetTestFilesByTestIdQuery { TestId = id });
            var model = new GetTestModelResponse
            {
                Test = test,
                Files = files
            };
            return View(model);

        }
        public async Task<IActionResult> Submission(int testId = 0)
        {
            string studentId = "20T1020433";
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = studentId });

            if (submission != null)
            {
                var comments = _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = submission.SubmissionId });

                var model = new GetSubmissionModelResponse
                {
                    Submission = submission,
                    Comments = comments
                };

                return PartialView(model);
            }
            return RedirectToAction("Index", "StudentHome");
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
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id});
            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                GetTestByIdResponse test = await _mediator.Send( new GetTestByIdQuery { Id = submission.TestId });
                if (test!.IsConductedAtSchool && !Utils.CheckIPAddress(ipAddress))
                {
                    return BadRequest("Địa chỉ IP không hợp lệ. Bạn không được phép nộp bài.");
                }
                SubmitTestCommand command = new SubmitTestCommand
                {
                    SubmissionId = id,
                    IPAddress = ipAddress,
                    IsCheckIP = test.IsCheckIP,
                    SubmittedTime = DateTime.Now,
                    TestEndTime = test.EndTime
                };
                if (await _mediator.Send(command))
                {
                    submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
                    var model = new GetSubmissionModelResponse
                    {
                        Submission = submission,
                        Comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = id })
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
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });

            if (submission != null)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;

                if (await StudentService.Cancel(ipAddress, id))
                {
                    submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = id })
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
