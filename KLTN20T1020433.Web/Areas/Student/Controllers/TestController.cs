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
        const int PAGE_SIZE = 8;

        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index(string searchValue = "")
        {
            var user = User.GetUserData();
            var input = ApplicationContext.GetSessionData<GetTestsBySearchQuery>(Constants.TEST_SEARCH);
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
                var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id, TestStartTime = test.StartTime });
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


        public async Task<IActionResult> DownloadFile(Guid id, int testId = 0)
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
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test == null)
                {
                    return View("NotFound");
                }
                var file = await _mediator.Send(new GetTestFileByIdQuery { Id = id, TestId = testId, TestStartTime = test.StartTime });

                if (file == null || !System.IO.File.Exists(file.FilePath))
                {
                    return View("NotFound");
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

    }
}
