using AutoMapper;
using KLTN20T1020433.Application.Commands.StudentCommands.Create;
using KLTN20T1020433.Application.Commands.TeacherCommands.Create;
using KLTN20T1020433.Application.Commands.TeacherCommands.Delete;
using KLTN20T1020433.Application.Commands.TeacherCommands.Update;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Queries;
using KLTN20T1020433.Application.Queries.TeacherQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Teacher.Commands.Delete;
using KLTN20T1020433.Web.Areas.Teacher.Models;
using KLTN20T1020433.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Reflection;

namespace KLTN20T1020433.Web.Controllers.Teacher
{
    [Area("Teacher")]
    [Authorize]
    [TeacherOnlyFilter]
    public class TestController : Controller
    {
        const int TEST_PAGE_SIZE = 10;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TestController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherID = user.UserId });
                if (test == null)
                {
                    return View("NotFound");
                }
                var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id });
                var model = new TestModel()
                {
                    Test = test,
                    Files = files,
                };
                return View(model);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception occurred in Detail: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }

        }
        public async Task<IActionResult> ListSubmissions(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherID = user.UserId });
                if (test == null)
                {
                    return BadRequest(ErrorMessages.GeneralError);
                }
                var input = ApplicationContext.GetSessionData<GetSubmissionsBySearchQuery>(Constants.SUBMISSION_SEARCH);
                if (input == null)
                {
                    input = new GetSubmissionsBySearchQuery()
                    {
                        Page = 1,
                        PageSize = TEST_PAGE_SIZE,
                        SearchValue = "",
                        TestId = testId,
                        Status = null,
                    };
                }
                return View(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ListSubmissions: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CancelCreation(int testId)
        {
            try
            {
                await _mediator.Send(new DeleteTestCommand { Id = testId });
                return Json(SuccessMessages.CancelCreationSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in CancelCreation: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> SearchSubmission(GetSubmissionsBySearchQuery input)
        {
            try
            {
                var user = User.GetUserData();
                int rowCount = await _mediator.Send(new GetRowCountSubmissionsQuery { });

                var data = await _mediator.Send(input);

                var model = new SubmissonSearchResult()
                {
                    Page = input.Page,

                    PageSize = input.PageSize,
                    SearchValue = input.SearchValue ?? "",
                    TestId = input.TestId,
                    Status = input.Status ?? null,
                    RowCount = rowCount,
                    Data = data
                };
                ApplicationContext.SetSessionData(Constants.SUBMISSION_SEARCH, input);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in SearchSubmission: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [Route("Teacher/{type?}/Create")]
        public async Task<IActionResult> Create(string type = "")
        {
            try
            {
                var user = User.GetUserData();
                var testId = ApplicationContext.GetDataInt32(Constants.TESTID) ?? 0;
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherID = user.UserId });
                ViewBag.IsEdit = false;
                switch (type.ToLower())
                {
                    case "quiz":
                        ViewBag.Title = "Tạo bài kiểm tra";
                        test.TestType = TestType.Quiz;
                        return View("Edit", test);
                    case "exam":
                        ViewBag.Title = "Tạo kỳ thi";
                        test.TestType = TestType.Exam;
                        return View("Edit", test);
                    default:
                        return View("NotFound");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Create: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
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
                    PageSize = TEST_PAGE_SIZE,
                    SearchValue = searchValue,
                    Status = null,
                    Type = null,
                    FromTime = null,
                    ToTime = null,
                    TeacherId = user.UserId,
                    AcademicYear = "",
                    Semester = 0
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
                int rowCount = await _mediator.Send(new GetRowCountTestsQuery { TeacherId = user.UserId, SearchValue = input.SearchValue, Status = input.Status, FromTime = input.FromTime, ToTime = input.ToTime, Type = input.Type });

                var data = await _mediator.Send(input);

                var model = new TestSearchResult()
                {
                    Page = input.Page,
                    PageSize = input.PageSize,
                    SearchValue = input.SearchValue ?? "",
                    RowCount = rowCount,
                    FromTime = input.FromTime,
                    ToTime = input.ToTime,
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
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> SearchStudent(GetTestsBySearchQuery input)
        {

            var user = User.GetUserData();
            int rowCount = await _mediator.Send(new GetRowCountTestsQuery { TeacherId = user.UserId, SearchValue = input.SearchValue, Status = input.Status, FromTime = input.FromTime, ToTime = input.ToTime, Type = input.Type });

            var data = await _mediator.Send(input);

            var model = new TestSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                FromTime = input.FromTime ?? null,
                ToTime = input.ToTime ?? null,
                Status = input.Status ?? null,
                Type = input.Type ?? null,
                Data = data
            };

            // Lưu lại vào session điều kiện tìm kiếm
            ApplicationContext.SetSessionData(Constants.TEST_SEARCH, input);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveTestFile(Guid id)
        {
            try
            {
                var file = await _mediator.Send(new GetTestFileByIdQuery { Id = id });
                if (file == null)
                    return Json(ErrorMessages.FileNotFound);
                else
                {
                    var message = await _mediator.Send(new RemoveTestFileCommand { Id = id });
                    return Json(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in RemoveTestFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public IActionResult SelectCourses()
        {
            return View();
        }
        public async Task<IActionResult> Submission(int id = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (id <= 0)
                    return View("NotFound");
                var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
                if (submission == null)
                    return View("NotFound");
                var files = await _mediator.Send(new GetFilesBySubmissionIdQuery { SubmissionId = id });
                var model = new SubmissionModel
                {
                    Files = files,
                    Submission = submission
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in RemoveTestFile: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }
        public async Task<IActionResult> SubmissionFile(Guid fileId)
        {
            try
            {
                var user = User.GetUserData();
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = fileId });
                if (file == null)
                {
                    return NotFound();
                }
                switch (file.MimeType)
                {
                    case "image/jpeg":
                    case "image/png":
                    case "image/gif":
                        return File(file.FilePath, file.MimeType);
                    case "text/plain":
                    case "application/octet-stream":
                    case "application/vnd.openxmlformats-officedocument.word":
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    case "application/msword":
                        return PartialView(FileUtils.ReadDocxFile(file.FilePath));
                    default:
                        {
                            byte[] fileBytes = await FileUtils.ReadFileAsync(file.FilePath);
                            return File(fileBytes, file.MimeType, file.OriginalName);
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in RemoveTestFile: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            try
            {
                var user = User.GetUserData();
                ViewBag.IsEdit = true;
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherID = user.UserId });
                if (test == null)
                {
                    return View("NotFound");
                }
                switch (test.TestType)
                {
                    case TestType.Quiz:
                        ViewBag.Title = "Cập nhật thông tin bài kiểm tra";
                        return View(test);
                    case TestType.Exam:
                        ViewBag.Title = "Cập nhật thông tin kỳ thi";
                        return View(test);
                    default:
                        return View("NotFound");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Edit: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(int testId, string[] selectedStudents)
        {
            try
            {
                var user = User.GetUserData();
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherID = user.UserId });
                if (test == null)
                    return Json(ErrorMessages.TestNotFound);
                var message = await _mediator.Send(new CreateSubmissionCommand { TestId = testId, StudentIds = selectedStudents });
                return Json(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Save: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> Download(Guid id)
        {
            var user = User.GetUserData();
            bool isAuthorized = false; //await FileDataService.CheckFileAuthorize(studentId, id);
            if (isAuthorized)
            {
                var file = await _mediator.Send(new GetSubmissionFileByIdQuery { Id = id });

                if (file == null)
                {
                    return BadRequest();
                }
                string filePath = file.FilePath;
                string mimeType = file.MimeType;
                if (!System.IO.File.Exists(filePath))
                {
                    return Json("Không tìm thấy file");
                }
                byte[] fileBytes = await FileUtils.ReadFileAsync(filePath);
                return File(fileBytes, mimeType, file.OriginalName);
            }
            else
            {
                return Json("Bạn không có quyền truy cập file.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadTestFile(List<IFormFile> files)
        {
            try
            {
                var user = User.GetUserData();
                if (files == null || files.Count == 0)
                    return Json(ErrorMessages.NoFilesUploaded);
                else
                {
                    int? testId = ApplicationContext.GetDataInt32(Constants.TESTID);
                    if (testId == null || testId.Value == 0)
                    {
                        testId = await _mediator.Send(new CreateTestCommand { TeacherId = user.UserId, TestStatus = TestStatus.Creating });
                        ApplicationContext.SetInt32(Constants.TESTID, testId.Value);
                    }
                    else
                    {
                        testId = ApplicationContext.GetDataInt32(Constants.TESTID) ?? 0;
                    }

                    foreach (var item in files)
                    {
                        if (item == null || item.Length == 0 || item.Length >= FileUtils.MAX_FILE_SIZE)
                        {
                            return Json(new { success = false, message = ErrorMessages.InvalidOrLargeFile });
                        }
                        if (!(await _mediator.Send(new CreateTestFileCommand { File = item, TestId = testId.Value })))
                            return Json(new { success = false, message = ErrorMessages.FileUploadError });
                    }
                    return Json(new { success = true, testId = testId });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in UploadTestFile: {ex.Message}");
                return Json(new { success = false, message = ErrorMessages.RequestNotCompleted });
            }
        }
        [Route("Teacher/{type?}/SelectStudents")]
        public async Task<IActionResult> SelectStudents(string type, CreateTestCommand command)
        {
            try
            {
                var user = User.GetUserData();
                int? testId = ApplicationContext.GetDataInt32(Constants.TESTID);
                if (testId != null && testId != 0)
                {
                    var updateTestCommmand = _mapper.Map<UpdateTestCommand>(command);
                    updateTestCommmand.TestId = testId.Value;
                    await _mediator.Send(updateTestCommmand);
                    HttpContext.Session.Remove(Constants.TESTID);
                }
                else
                {
                    command.TeacherId = user.UserId;
                    command.TestStatus = TestStatus.Creating;
                    testId = await _mediator.Send(command);
                    HttpContext.Session.SetInt32(Constants.TESTID, testId.Value);
                }
                var token = ApplicationContext.GetSessionData<GetTokenResponse>(Constants.ACCESS_TOKEN);
                ViewBag.TestId = testId.Value;
                switch (type.ToLower())
                {
                    case "quiz":
                        var courses = await _mediator.Send(new GetCoursesByTeacherIdQuery { GetTokenResponse = token, TeacherId = user.UserId });
                        ViewBag.Title = "Tạo bài kiểm tra";
                        return View("QuizSelectStudents", courses);
                    case "exam":
                        var exams = await _mediator.Send(new GetExamsByTeacherIdQuery { GetTokenResponse = token, TeacherId = user.UserId });
                        ViewBag.Title = "Tạo kỳ thi";
                        return View("ExamSelectStudents", exams);
                    default:
                        return View("NotFound");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in SelectStudents: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }
        [Route("Teacher/EditStudents/{id?}")]
        public async Task<IActionResult> EditStudents(int id = 0)
        {
            try
            {
                var user = User.GetUserData();
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherID = user.UserId });
                var token = ApplicationContext.GetSessionData<GetTokenResponse>(Constants.ACCESS_TOKEN);
                switch (test.TestType)
                {
                    case TestType.Quiz:
                        var courses = await _mediator.Send(new GetCoursesByTeacherIdQuery { GetTokenResponse = token, TeacherId = user.UserId });
                        ViewBag.Title = "Chỉnh sửa bài kiểm tra";
                        return View("QuizSelectStudents", courses);
                    case TestType.Exam:
                        var exams = await _mediator.Send(new GetExamsByTeacherIdQuery { GetTokenResponse = token, TeacherId = user.UserId });
                        ViewBag.Title = "Chỉnh sửa kỳ thi";
                        return View("ExamSelectStudents", exams);
                    default:
                        return View("NotFound");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in EditStudents: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }
        public async Task<IActionResult> ListStudents(TestType type, string courseId)
        {
            ViewBag.IsSelectedList = false;
            var token = ApplicationContext.GetSessionData<GetTokenResponse>(Constants.ACCESS_TOKEN);
            IEnumerable<GetStudentResponse> students = new List<GetStudentResponse>();
            if (type == TestType.Quiz)
            {
                students = await _mediator.Send(new GetStudentsByCourseIdQuery { CourseId = courseId, GetTokenResponse = token });
            }
            else
            {
                students = await _mediator.Send(new GetStudentsByExamIdQuery { CourseId = courseId, GetTokenResponse = token });
            }
            return View(students);
        }
        public async Task<IActionResult> SelectedStudents(int testId)
        {
            ViewBag.IsSelectedList = true;
            IEnumerable<GetStudentResponse> students = await _mediator.Send(new GetStudentsByTestIdQuery { TestId = testId });
            return View("ListStudents", students);
        }
        public IActionResult QuizSelectStudents()
        {
            return View();
        }
        public IActionResult ExamSelectStudents()
        {
            return View();
        }
        public async Task<IActionResult> ListTestFiles(int testId = 0)
        {
            try
            {
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
                if (test != null)
                {
                    var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = testId });

                    var model = new TestFileModel
                    {
                        Status = test.Status,
                        Files = files

                    };
                    return View(model);
                }
                return BadRequest(ErrorMessages.GeneralError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in ListTestFiles: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
    }


}
