using AutoMapper;
using KLTN20T1020433.Application.Commands.TeacherCommands.Create;
using KLTN20T1020433.Application.Commands.TeacherCommands.Delete;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Queries;
using KLTN20T1020433.Application.Queries.TeacherQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Teacher.Commands.Update;
using KLTN20T1020433.Web.Areas.Teacher.Models;
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
            var user = User.GetUserData();
            var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherID = user.UserId });
            if (test.TestId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = id });
            var model = new TestModel()
            {
                Test = test,
                Files = files
            };
            return View(model);

        }
        public async Task<IActionResult> ListSubmission(int testId = 0)
        {
            var user = User.GetUserData();
            var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherID = user.UserId });
            if (test.TestId == 0)
            {
                return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> SearchSubmission(GetSubmissionsBySearchQuery input)
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

            // Lưu lại vào session điều kiện tìm kiếm
            ApplicationContext.SetSessionData(Constants.SUBMISSION_SEARCH, input);

            return View(model);
        }
        public async Task<IActionResult> CreateQuiz()
        {
            var user = User.GetUserData();
            ViewBag.Title = "Tạo bài kiểm tra";
            ViewBag.IsEdit = false;
            var testId = ApplicationContext.GetDataInt32(Constants.TESTID) ?? 0;
            var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherID = user.UserId });
            test.TestType = TestType.Quiz;
            return View("Edit", test);
        }
        public async Task<IActionResult> CreateExam()
        {
            var user = User.GetUserData();
            ViewBag.Title = "Tạo kỳ thi";
            ViewBag.IsEdit = false;
            var testId = ApplicationContext.GetDataInt32(Constants.TESTID) ?? 0;
            var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherID = user.UserId });
            test.TestType = TestType.Exam;
            return View("Edit", test);
        }
        public IActionResult ListTest(string searchValue = "")
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
            var file = await _mediator.Send(new GetTestFileByIdQuery { Id = id });
            if (file == null)
                return Json("Không tìm thấy file");
            else
            {
                await _mediator.Send(new RemoveTestFileCommand { });
            }
            var testId = ApplicationContext.GetDataInt32(Constants.TESTID) ?? 0;
            return Json(testId);
        }
        public IActionResult SelectCourses()
        {
            return View();
        }
        public async Task<IActionResult> Submission(int id = 0)
        {
            var user = User.GetUserData();
            var submission = await _mediator.Send(new GetSubmissionByIdQuery { Id = id });
            var files = await _mediator.Send(new GetFilesBySubmissionIdQuery { SubmissionId = id });
            var model = new SubmissionModel
            {
                Files = files,
                Submission = submission
            };
            return View(model);
        }
        public async Task<IActionResult> SubmissionFile(Guid fileId)
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
        public async Task<IActionResult> Edit(int id = 0)
        {
            var user = User.GetUserData();
            ViewBag.IsEdit = true;
            var test = await _mediator.Send(new GetTestByIdQuery { Id = id, TeacherID = user.UserId });
            if (test == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                switch (test.TestType)
                {
                    case TestType.Quiz:
                        ViewBag.Title = "Cập nhật thông tin bài kiểm tra";
                        return View(test);
                    case TestType.Exam:
                        ViewBag.Title = "Cập nhật thông tin kỳ thi";
                        return View(test);
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }

        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(string[] selectedStudents)
        {
            var user = User.GetUserData();
            var testId = ApplicationContext.GetDataInt32(Constants.TESTID) ?? 0;
            var test = await _mediator.Send(new GetTestByIdQuery { Id = testId, TeacherID = user.UserId });
            if (test.TestId == 0)
                return BadRequest("Không tìm thấy kỳ thi");
            else
            {
                await _mediator.Send(new CreateSubmissionCommand { TestId = testId, StudentIds = selectedStudents });
                return RedirectToAction("ListTest");
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
            var user = User.GetUserData();
            if (files == null || files.Count == 0)
                return Json("Không có tệp nào được gửi.");
            else
            {
                int testId = 0;
                if (ApplicationContext.GetDataInt32(Constants.TESTID) == null || ApplicationContext.GetDataInt32(Constants.TESTID) == 0)
                {
                    testId = await _mediator.Send(new CreateTestCommand { TeacherId = user.UserId, TestType = TestType.Exam, TestStatus = TestStatus.Creating });
                    ApplicationContext.SetInt32(Constants.TESTID, testId);
                }
                else
                {
                    testId = ApplicationContext.GetDataInt32(Constants.TESTID) ?? 0;
                }

                foreach (var item in files)
                {

                    await _mediator.Send(new CreateTestFileCommand { File = item, TestId = testId });
                }
                return Json(testId);
            }
        }
        [Route("Test/SelectStudents/{type?}")]
        public async Task<IActionResult> SelectStudents(string type, CreateTestCommand command)
        {
            var user = User.GetUserData();
            if (ApplicationContext.GetDataInt32(Constants.TESTID) != null && ApplicationContext.GetDataInt32(Constants.TESTID) != 0)
            {
                var updateTestCommmand = _mapper.Map<UpdateTestCommand>(command);
                await _mediator.Send(updateTestCommmand);
            }
            else
            {
                int testId = await _mediator.Send(command);
                HttpContext.Session.SetInt32(Constants.TESTID, testId);
            }
            var token = ApplicationContext.GetSessionData<GetTokenResponse>(Constants.ACCESS_TOKEN);
            switch (type)
            {
                case "quiz":
                    var courses = _mediator.Send(new GetCoursesByTeacherIdQuery { GetTokenResponse = token, TeacherId = user.UserId });
                    return View("QuizSelectStudents", courses);
                case "exam":
                    var exams = _mediator.Send(new GetExamsByTeacherIdQuery { GetTokenResponse = token, TeacherId = user.UserId });
                    return View("ExamSelectStudents", exams);
                default:
                    return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> ListStudents(TestType type, string courseId)
        {
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
            //testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
            var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
            if (test != null)
            {
                var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = testId });

                var model = new TestFileModel
                {
                    Status = test.Status,
                    Files = files

                };
                return PartialView(model);
            }
            return BadRequest("Có lỗi xảy ra.");
        }
    }


}
