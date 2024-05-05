using KLTN20T1020433.Application.Commands.StudentCommands.Create;
using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Application.Commands.StudentCommands.Update;
using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Student.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Controllers.Student
{
    [Area("Student")]
    [Authorize]
    public class TestController : Controller
    {
        const int PAGE_SIZE = 10;

        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index(string searchValue = "")
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
            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = id, StudentId = user.UserId! });
            if (submission == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var test = await _mediator.Send(new GetTestByIdQuery { Id = id });
            if (test == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IEnumerable<GetTestFileResponse> files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id });
            var model = new TestModel
            {
                Test = test,
                Files = files
            };
            return View(model);
        }

        public async Task<IActionResult> Submission(int testId = 0)
        {
            var user = User.GetUserData();

            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
            if (submission != null)
            {
                var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = submission.SubmissionId });
                var model = new SubmissionModel
                {
                    Submission = submission,
                    Comments = comments
                };

                return PartialView(model);
            }
            return BadRequest("Không tìm thấy bài nộp.");
        }
        [HttpPost]
        public async Task<IActionResult> UploadSubmissionFile(List<IFormFile> files, int testId = 0)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
            if (submission == null)
            {
                return Json("Có lỗi xảy ra");
            }
            if (files == null || files.Count == 0)
                return Json("Không có tệp nào được gửi.");
            foreach (var item in files)
            {
                if (item == null || item.Length == 0 || item.Length >= FileUtils.MAX_FILE_SIZE)
                {
                    return Json("File không hợp lệ hoặc có kích thước quá lớn.");
                }
                if (!(await _mediator.Send(new CreateSubmissionFileCommand { File = item, SubmissionId = submission.SubmissionId })))
                    return Json("Có lỗi khi lưu file");
            }
            return Json("Tải file lên thành công.");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSubmissionFile(Guid id, int testId = 0)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
            if (submission == null)
            {
                return Json("Có lỗi xảy ra.");
            }
            var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
            if (file == null)
                return Json("Không tìm thấy file");
            else
            {
                var message = await _mediator.Send(new RemoveSubmissionFileCommand { Id = id });
                return Json(message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Submit(int testId)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
            if (submission == null)
            {
                return Json("Có lỗi xảy ra.");
            }
            var test = await _mediator.Send(new GetTestByIdQuery { Id = submission.TestId });
            if (test!.IsConductedAtSchool && !Utils.CheckIPAddress(user.ClientIP!))
            {
                return Json("Địa chỉ IP không hợp lệ. Bạn không được phép nộp bài.");
            }
            if (!test.CanSubmitLate && DateTime.Now > test.EndTime)
            {
                return Json("Đã quá thời gian cho phép nộp bài. Bạn không được phép nộp bài.");
            }
            var message = await _mediator.Send(new SubmitTestCommand
            {
                SubmissionId = submission.SubmissionId,
                IPAddress = user.ClientIP!,
                IsCheckIP = test.IsCheckIP,
                SubmittedTime = DateTime.Now,
                TestEndTime = test.EndTime
            });
            return Json(message);
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int testId)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId });
            if (submission == null)
            {
                return Json("Có lỗi xảy ra.");
            }
            var message = await _mediator.Send(new CancelSubmissionCommand { SubmissionId = submission.SubmissionId });
            return Json(message);
        }
        public async Task<IActionResult> DownloadSubmissionFile(Guid id, int testId)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
            if (submission == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
            if (file == null || !System.IO.File.Exists(file.FilePath))
            {
                return Json("Không tìm thấy file");
            }
            byte[] fileBytes = await FileUtils.ReadFileAsync(file.FilePath);
            return File(fileBytes, file.MimeType, file.OriginalName);
        }
        public async Task<IActionResult> DownloadTestFile(Guid id, int testId)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
            if (submission == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var file = await _mediator.Send(new GetTestFileByIdQuery { Id = id });
            if (file == null || !System.IO.File.Exists(file.FilePath))
            {
                return Json("Không tìm thấy file");
            }
            byte[] fileBytes = await FileUtils.ReadFileAsync(file.FilePath);
            return File(fileBytes, file.MimeType, file.OriginalName);
        }
        public async Task<IActionResult> ListSubmissionFiles(int testId = 0)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId });
            if (submission != null)
            {
                var files = await _mediator.Send(new GetFilesBySubmissionIdQuery { SubmissionId = submission.SubmissionId });
                var model = new SubmissionFileModel
                {
                    Files = files,
                    TestId = testId,
                    Status = submission.Status
                };
                return PartialView(model);
            }
            return BadRequest("Không tìm thấy bài nộp.");
        }

    }
}
