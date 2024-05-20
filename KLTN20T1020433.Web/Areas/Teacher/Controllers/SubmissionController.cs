using AutoMapper;
using KLTN20T1020433.Application.Queries.TeacherQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Teacher.Models;
using KLTN20T1020433.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize]
    [TeacherOnlyFilter]
    public class SubmissionController : Controller
    {

        const int SUBMISSION_PAGE_SIZE = 40;
        private readonly IMediator _mediator;

        public SubmissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Search(GetSubmissionsBySearchQuery input, bool searchInDetail = true)
        {
            try
            {
                var user = User.GetUserData();
                var test = await _mediator.Send(new GetTestDetailQuery { Id = input.TestId, TeacherId = user.UserId });
                if (test == null)
                {
                    return View("NotFound");
                }
                int rowCount = await _mediator.Send(new GetRowCountSubmissionsQuery { SearchValue = input.SearchValue, Statuses = input.Statuses, TestId = input.TestId });

                var data = await _mediator.Send(input);

                var model = new SubmissonSearchResult()
                {
                    Page = input.Page,
                    PageSize = input.PageSize,
                    SearchValue = input.SearchValue ?? "",
                    TestId = input.TestId,
                    Statuses = input.Statuses ?? "",
                    RowCount = rowCount,
                    Data = data
                };
                ApplicationContext.SetSessionData(Constants.SUBMISSION_SEARCH, input);
                return searchInDetail ? View(model) : View("SearchStudent", model);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in SearchSubmission: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> Detail(int id = 0, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (id <= 0 || testId <= 0)
                    return View("NotFound");
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test == null)
                {
                    return View("NotFound");
                }
                var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
                if (submission == null)
                    return View("NotFound");
                var files = await _mediator.Send(new GetFilesBySubmissionIdQuery { SubmissionId = id, Status = submission.Status });
                if (files == null || !files.Any())
                {
                    return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.FileNotFound });
                }
                var input = ApplicationContext.GetSessionData<GetSubmissionsBySearchQuery>(Constants.STUDENT_SEARCH);
                if (input == null)
                {
                    input = new GetSubmissionsBySearchQuery()
                    {
                        Page = 1,
                        PageSize = SUBMISSION_PAGE_SIZE,
                        SearchValue = "",
                        TestId = testId,
                        Statuses = ""
                    };
                }
                var model = new SubmissionModel
                {
                    TestId = test.TestId,
                    Title = test.Title,
                    Files = files,
                    Submission = submission,
                    SearchQuery = input

                }; ApplicationContext.SetSessionData(Constants.SUBMISSION_SEARCH, input);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in SubmissionDetail: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }
        public async Task<IActionResult> File(Guid fileId, int testId = 0, int submissionId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return View("NotFound");
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test == null)
                {
                    return View("NotFound");
                }
                var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = submissionId });
                if (submission == null)
                    return View("NotFound");
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = fileId , Status = submission.Status});
                if (file == null)
                {
                    return View("NotFound");
                }
                var fileViewModel = new FileViewModel
                {
                    FileId = file.FileId,
                    FileName = file.OriginalName,
                    FilePath = file.FilePath,
                    MimeType = file.MimeType,
                    Content = file.MimeType.StartsWith("text") || file.MimeType.Contains("word")
                        ? FileUtils.ConvertToHtmlAsync(file.FilePath)
                        : null,
                    IsImage = file.MimeType.StartsWith("image"),
                    IsText = file.MimeType.StartsWith("text") || file.MimeType.Contains("word"),
                    SubmissionId = submissionId
                };

                return View(fileViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in SubmissionFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> CountSubmission(GetRowCountSubmissionsQuery input)
        {
            try
            {
                int count = await _mediator.Send(new GetRowCountSubmissionsQuery { SearchValue = input.SearchValue, Statuses = input.Statuses, TestId = input.TestId });
                return Json(new { success = true, count = count });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in CountSubmission: {ex.Message}");
                return Json(new { success = false, count = 0 });
            }
        }
        public async Task<IActionResult> DownloadAllSubmissionFile(int testId)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return View("NotFound");
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test == null)
                    return View("NotFound");
                string zipName = $"{test.Title.RemoveDiacritics().Trim()}.zip";
                var submissionFiles = await _mediator.Send(new GetSubmissionFilesByTestIdQuery { TestId = testId });
                var archiveStream = FileUtils.ZipFiles(submissionFiles);
                return File(archiveStream, "application/zip", zipName);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred in DownloadAllSubmissionFile: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }
        public async Task<ActionResult> DownloadFile(Guid id, int submissionId)
        {
            try
            {
                var user = User.GetUserData();
                if (submissionId <= 0)
                {
                    return View("NotFound");
                }
                var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = submissionId });
                if (submission == null)
                    return View("NotFound");
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id, Status = submission.Status });
                if (file == null)
                {
                    return View("NotFound");
                }
                string filePath = file.FilePath;
                string mimeType = file.MimeType;
                if (!System.IO.File.Exists(filePath))
                {
                    return View("NotFound");
                }
                byte[] fileBytes = await FileUtils.ReadFileAsync(filePath);
                return File(fileBytes, mimeType, file.OriginalName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }
    }
}
