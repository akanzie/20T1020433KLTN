using AutoMapper;
using KLTN20T1020433.Application.Commands.TeacherCommands.Create;
using KLTN20T1020433.Application.Commands.TeacherCommands.Delete;
using KLTN20T1020433.Application.Commands.TeacherCommands.Update;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Queries;
using KLTN20T1020433.Application.Queries.TeacherQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Teacher.Commands.Delete;
using KLTN20T1020433.Web.Areas.Teacher.Models;
using KLTN20T1020433.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Controllers.Teacher
{
    [Area("Teacher")]
    [Authorize]
    [TeacherOnlyFilter]
    public class TestController : Controller
    {
        const int TEST_PAGE_SIZE = 7;
        const int SUBMISSION_PAGE_SIZE = 40;
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
                var test = await _mediator.Send(new GetTestDetailQuery { Id = id, TeacherId = user.UserId });
                if (test == null)
                {
                    return View("NotFound");
                }
                var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id });
                var input = ApplicationContext.GetSessionData<GetSubmissionsBySearchQuery>(Constants.SUBMISSION_SEARCH);
                if (input == null)
                {
                    input = new GetSubmissionsBySearchQuery()
                    {
                        Page = 1,
                        PageSize = SUBMISSION_PAGE_SIZE,
                        SearchValue = "",
                        TestId = id,
                        Statuses = ""
                    };
                }
                var model = new TestDetailModel()
                {
                    Test = test,
                    Files = files,
                    SearchQuery = input
                };
                ApplicationContext.SetSessionData(Constants.SUBMISSION_SEARCH, input);
                return View(model);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception occurred in Detail: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }

        }

        [HttpPost]
        public async Task<IActionResult> CancelCreation(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    if (ApplicationContext.GetDataInt32(Constants.TESTID) == null)
                        return Json(ErrorMessages.GeneralError);
                    testId = ApplicationContext.GetDataInt32(Constants.TESTID).Value;
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test == null)
                    return Json(ErrorMessages.GeneralError);
                if (test.Status == TestStatus.Creating)
                    await _mediator.Send(new DeleteTestCommand { Id = testId });
                return Json(SuccessMessages.CancelCreationSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in CancelCreation: {ex.Message}");
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
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test == null)
                    test = new GetTestByIdResponse();
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
                int rowCount = await _mediator.Send(new GetRowCountTestsQuery
                {
                    TeacherId = user.UserId,
                    SearchValue = input.SearchValue,
                    Status = input.Status,
                    FromTime = input.FromTime,
                    ToTime = input.ToTime,
                    Type = input.Type
                });

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

        public async Task<IActionResult> Edit(int id = 0)
        {
            try
            {
                var user = User.GetUserData();
                ViewBag.IsEdit = true;
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherId = user.UserId });
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
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(int[] testIds)
        {
            try
            {
                if (testIds == null || testIds.Length <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var user = User.GetUserData();
                foreach (var item in testIds)
                {
                    await _mediator.Send(new DeleteTestCommand { Id = item });
                }
                return Json(SuccessMessages.DeleteTestSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Save: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id = 0)
        {
            try
            {
                if (id <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var user = User.GetUserData();
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherId = user.UserId });
                if (test == null)
                    return Json(ErrorMessages.TestNotFound);
                await _mediator.Send(new DeleteTestCommand { Id = id });
                return Json(SuccessMessages.DeleteTestSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Save: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save(List<StudentSelection> students, int testId = 0, string semester = "", string moduleId = "")
        {
            try
            {
                if (testId <= 0)
                {
                    return Json(new { success = false, message = ErrorMessages.GeneralError });
                }
                if (students.Count() == 0)
                {
                    return Json(new { success = false, message = ErrorMessages.ListStudentsIsEmpty });
                }
                var user = User.GetUserData();
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test == null)
                    return Json(new { success = false, message = ErrorMessages.TestNotFound });
                var updateTest = _mapper.Map<UpdateTestCommand>(test);
                updateTest.Status = TestStatus.InProgress;
                updateTest.Semester = semester;
                updateTest.ModuleId = moduleId;
                await _mediator.Send(updateTest);
                await _mediator.Send(new CreateSubmissionCommand { TestId = testId, Students = students });
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Save: {ex.Message}");
                return Json(new { success = false, message = ErrorMessages.RequestNotCompleted });
            }
        }
        public async Task<ActionResult> DownloadFile(Guid id, int testId)
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
                var file = await _mediator.Send(new GetTestFileByIdQuery { Id = id });

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

        [HttpPost]
        public async Task<IActionResult> UploadTestFile(List<IFormFile> files, int? testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (files == null || files.Count == 0)
                    return Json(ErrorMessages.NoFilesUploaded);
                else
                {
                    if (testId == 0)
                    {
                        testId = ApplicationContext.GetDataInt32(Constants.TESTID);
                        if (testId == null || testId.Value == 0)
                        {
                            testId = await _mediator.Send(new CreateTestCommand { TeacherId = user.UserId, TestStatus = TestStatus.Creating });
                            ApplicationContext.SetInt32(Constants.TESTID, testId.Value);
                        }
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
                var schoolYears = await _mediator.Send(new GetSchoolYearQuery { });
                switch (type.ToLower())
                {
                    case "quiz":
                        ViewBag.Title = "Tạo bài kiểm tra";
                        return View("QuizSelectStudents", new SelectStudentModel { SchoolYears = schoolYears, TestId = testId.Value });
                    case "exam":
                        ViewBag.Title = "Tạo kỳ thi";
                        return View("ExamSelectStudents", new SelectStudentModel { SchoolYears = schoolYears, TestId = testId.Value });
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
        public async Task<IActionResult> EditStudents(UpdateTestCommand updateTest, int id = 0)
        {
            try
            {
                if (id <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var user = User.GetUserData();
                updateTest.TestId = id;
                updateTest.Status = TestStatus.InProgress;
                await _mediator.Send(updateTest);
                HttpContext.Session.Remove(Constants.TESTID);
                var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherId = user.UserId });
                var module = await _mediator.Send(new GetModuleByIdQuery { ModuleId = test.ModuleId, Signature = user.Signature, Token = user.Token });
                var schoolYears = await _mediator.Send(new GetSchoolYearQuery { });
                var semester = Utils.ParseSemester(test.Semester);
                switch (test.TestType)
                {
                    case TestType.Quiz:
                        ViewBag.Title = "Chỉnh sửa bài kiểm tra";
                        return View("QuizSelectStudents", new SelectStudentModel { Module = module, Semester = semester.Semester, SchoolYear = semester.SchoolYear, SchoolYears = schoolYears, TestId = id });
                    case TestType.Exam:
                        ViewBag.Title = "Chỉnh sửa kỳ thi";
                        return View("ExamSelectStudents", new SelectStudentModel { Module = module, Semester = semester.Semester, SchoolYear = semester.SchoolYear, SchoolYears = schoolYears, TestId = id });
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
        public async Task<IActionResult> GetComments(int submissionId = 0, int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (submissionId <= 0 || testId <= 0)
                    return View("NotFound");
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test == null)
                {
                    return View("NotFound");
                }
                var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = submissionId });
                if (submission == null)
                    return View("NotFound");
                var comments = await _mediator.Send(new GetCommentsBySubmissionIdQuery { SubmissionId = submissionId });

                return View("Comments", new CommentModel { Comments = comments });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in GetStudents: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> GetStudents(TestType type, string courseId = "", string examId = "")
        {
            try
            {
                var user = User.GetUserData();
                IEnumerable<GetStudentResponse> students = new List<GetStudentResponse>();
                if (type == TestType.Quiz)
                {
                    students = await _mediator.Send(new GetStudentsByCourseIdQuery { CourseId = courseId, Token = user.Token, Signature = user.Signature });
                }
                else
                {
                    students = await _mediator.Send(new GetStudentsByExamIdQuery { ExamId = examId, Token = user.Token, Signature = user.Signature });
                }
                return View("SearchStudents", new StudentModel { Students = students });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in GetStudents: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> GetSelectedStudents(int testId = 0)
        {
            try
            {

                if (testId <= 0)
                {
                    return Json(ErrorMessages.GeneralError);
                }
                var students = await _mediator.Send(new GetStudentsByTestIdQuery
                {
                    TestId = testId

                });
                return View("SearchStudents", new StudentModel { Students = students });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in GetSelectedStudents: {ex.Message}");
                return BadRequest(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> SearchStudents(string searchValue = "")
        {
            try
            {
                var user = User.GetUserData();
                var students = await _mediator.Send(new GetStudentsBySearchQuery { SearchValue = searchValue });

                return View(new StudentModel { Students = students });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in SearchStudent: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }

        public async Task<IActionResult> ListTestFiles(int testId = 0)
        {
            try
            {
                var user = User.GetUserData();
                if (testId <= 0)
                {
                    return View(new TestFileModel());
                }
                var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherId = user.UserId });
                if (test != null)
                {
                    var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = testId });

                    var model = new TestFileModel
                    {
                        TestId = testId,
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
        public async Task<IActionResult> Comment(CreateCommentCommand command)
        {
            try
            {
                var user = User.GetUserData();
                command.TeacherId = user.UserId;
                var message = await _mediator.Send(command);
                return Json(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in SelectStudents: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> GetCourses(string semester, string moduleId)
        {
            try
            {
                var user = User.GetUserData();
                var courses = await _mediator.Send(new GetCoursesByTeacherIdQuery { ModuleId = moduleId, Semester = semester, Signature = user.Signature, Token = user.Token });
                return Json(courses);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in GetStudents: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
        public async Task<IActionResult> GetModules(string semester)
        {
            try
            {
                var user = User.GetUserData();
                var modules = await _mediator.Send(new GetModulesQuery { Semester = semester, Signature = user.Signature, Token = user.Token });
                return Json(modules);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in GetStudents: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
    }


}
