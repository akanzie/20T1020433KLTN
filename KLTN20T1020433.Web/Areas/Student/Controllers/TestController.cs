using KLTN20T1020433.Application.Commands.StudentCommands.Create;
using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Application.Commands.StudentCommands.Update;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Controllers.Student
{
    [Area("Student")]
    [Authorize]
    public class TestController : Controller
    {
        const int PAGE_SIZE = 1;

        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult ListTest(string searchValue = "")
        {
            var user = User.GetUserData();
            GetTestsBySearchQuery? input = ApplicationContext.GetSessionData<GetTestsBySearchQuery>(Constants.TEST_SEARCH);
            if (input == null)
            {
                input = new GetTestsBySearchQuery()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    AcademicYear = "",
                    Semester = 0,
                    FromTime = null,
                    ToTime = null,
                    SearchValue = searchValue,
                    Status = null,
                    Type = null,
                    StudentId = user.UserId
                };
            }
            else
            {
                input.SearchValue = searchValue;
            }
            ApplicationContext.SetSessionData(Constants.TEST_SEARCH, input);
            return View(input);
        }
        public async Task<IActionResult> Search(GetTestsBySearchQuery input)
        {
            var user = User.GetUserData();
            var data = await _mediator.Send(input);
            int rowCount = await _mediator.Send(new GetRowCountQuery
            {
                StudentId = user.UserId,
                SearchValue = input.SearchValue,
                Status = input.Status,
                FromTime = input.FromTime,
                ToTime = input.ToTime,
                Type = input.Type
            });
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
            ApplicationContext.SetSessionData(Constants.TEST_SEARCH, input);

            return View(model);

        }
        public async Task<IActionResult> Detail(int id = 0)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = id, StudentId = user.UserId });
            if (submission.SubmissionId != 0)
            {
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id });
                if (test.TestId != 0)
                {
                    IEnumerable<GetTestFileResponse> files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id });
                    var model = new TestModel
                    {
                        Test = test,
                        Files = files
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Submission(int testId = 0)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId });

            if (submission.SubmissionId != 0)
            {
                var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = submission.SubmissionId });

                var model = new SubmissionModel
                {
                    Submission = submission,
                    Comments = comments
                };

                return PartialView(model);
            }
            return Json("Không tìm thấy bài nộp.");
        }
        [HttpPost]
        public async Task<IActionResult> UploadSubmissionFile(List<IFormFile> files, int submissionId = 0)
        {
            if (files == null || files.Count == 0)
                return Json("Không có tệp nào được gửi.");
            else
            {
                foreach (var item in files)
                {
                    if (!(await _mediator.Send(new CreateSubmissionFileCommand { File = item, SubmissionId = submissionId })))
                        return Json("Có lỗi khi lưu file");
                }
                return Json("Tải file lên thành công.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSubmissionFile(Guid id)
        {
            var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
            if (file.FileId == null)
                return Json("Không tìm thấy file");
            else
            {
                if (await _mediator.Send(new RemoveSubmissionFileCommand { Id = id, FilePath = file.FilePath }))
                    return Json("Xóa file thành công.");
                return Json("Có lỗi khi xóa file");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Submit(int id)
        {
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
            if (submission.SubmissionId != 0)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                var test = await _mediator.Send(new GetTestByIdQuery { Id = submission.TestId });
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
                    var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = id });
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = comments
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

            if (submission.SubmissionId != 0)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress;

                if (await _mediator.Send(new CancelSubmissionCommand { SubmissionId = id }))
                {
                    submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
                    var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = id });
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = comments
                    };
                    return PartialView("Submission", model);
                }
            }
            return BadRequest("Có lỗi xảy ra.");
        }
        public async Task<IActionResult> Download(Guid id)
        {
            var user = User.GetUserData();
            bool isAuthorized = false;// await FileDataService.CheckFileAuthorize(studentId, id);
            if (isAuthorized)
            {
                GetSubmissionFileResponse fileInfo = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
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
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = submissionId });
            if (submission.SubmissionId != 0)
            {
                var files = await _mediator.Send(new GetFilesBySubmissionIdQuery { SubmissionId = submissionId });
                var model = new SubmissionFileModel
                {
                    Files = files,
                    Status = submission.Status
                };
                return PartialView(model);
            }
            return BadRequest("Có lỗi xảy ra.");
        }
    }
}
