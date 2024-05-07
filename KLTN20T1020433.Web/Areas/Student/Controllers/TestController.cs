using KLTN20T1020433.Application.Commands.StudentCommands.Create;
using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Application.Commands.StudentCommands.Update;
using KLTN20T1020433.Application.DTOs;
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Search: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> Detail(int id = 0)
        {
            try
            {

                var user = User.GetUserData();
                if (id <= 0)
                {
                    return View("NotFound");
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = id, StudentId = user.UserId! });
                if (submission == null)
                {
                    return View("NotFound");
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id });
                if (test == null)
                {
                    return View("NotFound");
                }
                IEnumerable<GetTestFileResponse> files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id });
                var model = new TestModel
                {
                    Test = test,
                    Files = files
                };
                return View(model);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Detail: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }

        public async Task<IActionResult> Submission(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return BadRequest(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission != null)
                {
                    var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = submission.SubmissionId });
                    var model = new SubmissionModel
                    {
                        Submission = submission,
                        Comments = comments
                    };

                    return View(model);
                }
                return BadRequest(ErrorMessages.GeneralError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Detail: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadSubmissionFile(List<IFormFile> files, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                if (files == null || files.Count == 0)
                    return Json(ErrorMessages.NoFilesUploaded);
                foreach (var item in files)
                {
                    if (item == null || item.Length == 0 || item.Length >= FileUtils.MAX_FILE_SIZE)
                    {
                        return Json(ErrorMessages.InvalidOrLargeFile);
                    }
                    if (!(await _mediator.Send(new CreateSubmissionFileCommand { File = item, SubmissionId = submission.SubmissionId })))
                        return Json(ErrorMessages.FileUploadError);
                }
                return Json(SuccessMessages.FileUploadSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in UploadSubmissionFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveSubmissionFile(Guid id, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
                if (file == null)
                    return Json(ErrorMessages.FileNotFound);
                else
                {
                    var message = await _mediator.Send(new RemoveSubmissionFileCommand { Id = id });
                    return Json(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in RemoveSubmissionFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Submit(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = submission.TestId });
                if (test!.IsConductedAtSchool && !Utils.CheckIPAddress(user.ClientIP!))
                {
                    return Json(ErrorMessages.InvalidIPAddress);
                }
                if (!test.CanSubmitLate && DateTime.Now > test.EndTime)
                {
                    return Json(ErrorMessages.SubmissionTimeExceeded);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Submit: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId });
                if (submission == null)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var message = await _mediator.Send(new CancelSubmissionCommand { SubmissionId = submission.SubmissionId });
                return Json(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Cancel: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> DownloadSubmissionFile(Guid id, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return View("NotFound");
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return View("NotFound");
                }
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });
                if (file == null || !System.IO.File.Exists(file.FilePath))
                {
                    return Json(ErrorMessages.FileNotFound);
                }
                byte[] fileBytes = await FileUtils.ReadFileAsync(file.FilePath);
                return File(fileBytes, file.MimeType, file.OriginalName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in DownloadSubmissionFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> DownloadTestFile(Guid id, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return View("NotFound");
                }
                var submission = await _mediator.Send(new GetSubmissionByStudentIdAndTestIdQuery { TestId = testId, StudentId = user.UserId! });
                if (submission == null)
                {
                    return View("NotFound");
                }
                var file = await _mediator.Send(new GetTestFileByIdQuery { Id = id });
                if (file == null || !System.IO.File.Exists(file.FilePath))
                {
                    return Json(ErrorMessages.FileNotFound);
                }
                byte[] fileBytes = await FileUtils.ReadFileAsync(file.FilePath);
                return File(fileBytes, file.MimeType, file.OriginalName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in DownloadTestFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> ListSubmissionFiles(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return BadRequest(ErrorMessages.GeneralError);
                }
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
                    return View(model);
                }
                return BadRequest(ErrorMessages.GeneralError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ListSubmissionFiles: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }

    }
}
