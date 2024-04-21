using KLTN20T1020433.Web.AppCodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using KLTN20T1020433.Domain.Test;
using MediatR;
using KLTN20T1020433.Application.Queries.TeacherQueries;
using KLTN20T1020433.Web.Areas.Teacher.Models;
using KLTN20T1020433.Application.Commands.TeacherCommands.Create;
using KLTN20T1020433.Web.Areas.Teacher.Commands.Update;
using AutoMapper;
using KLTN20T1020433.Application.Services;

namespace KLTN20T1020433.Web.Controllers.Teacher
{
    public class TestController : Controller
    {
        const int TEST_PAGE_SIZE = 10;
        const string TEST_SEARCH = "test_search";
        const string TESTID = "testId";
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TestController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Detail(int testId = 0)
        {
            var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
            if (test.TestId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var files = await _mediator.Send(new GetFilesByTestIdQuery { TestId = testId });
            var model = new TestModel()
            {
                Test = test,
                Files = files
            };

            return View(model);

        }
        public async Task<IActionResult> ListSubmissionAsync(int testId = 0)
        {
            var test = await _mediator.Send(new GetTestByIdQuery { Id = testId });
            if (test == null)
            {
                return RedirectToAction("Index");
            }

            var model = await _mediator.Send(new GetSubmissionsByTestIdQuery { TestId = testId });
            return View(model);
        }
        public IActionResult CreateQuiz()
        {
            ViewBag.Title = "Tạo bài kiểm tra";
            ViewBag.IsEdit = false;
            var testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
            var model = new Test()
            {
                TestId = testId,
                TestType = TestType.Quiz
            };

            return View("CreateExam", model);
        }
        public IActionResult CreateExam()
        {
            ViewBag.Title = "Tạo bài thi";
            ViewBag.IsEdit = false;
            var testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
            var model = new CreateTestCommand()
            {
                TestType = TestType.Exam
            };

            return View("CreateExam", model);
        }
        public IActionResult ListTest()
        {
            var input = ApplicationContext.GetSessionData<GetTestsBySearchQuery>(TEST_SEARCH);
            if (input == null)
            {
                input = new GetTestsBySearchQuery()
                {
                    Page = 1,
                    PageSize = TEST_PAGE_SIZE,
                    SearchValue = "",
                    Status = null,
                    Type = null,
                    FromTime = null,
                    ToTime = null,
                    //ToTime = string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",DateTime.Today.AddMonths(-1), DateTime.Today)
                };
            }

            return View(input);
        }
        public async Task<IActionResult> Search(GetTestsBySearchQuery input)
        {
            string teacherId = "";
            int rowCount = await _mediator.Send(new GetRowCountQuery { TeacherId = input.TeacherId, SearchValue = input.SearchValue, Status = input.Status, FromTime = input.FromTime, ToTime = input.ToTime, Type = input.Type });

            var data = await _mediator.Send(input);

            var model = new TestSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            // Lưu lại vào session điều kiện tìm kiếm
            ApplicationContext.SetSessionData(TEST_SEARCH, input);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveTestFile(Guid id)
        {
            var file = await _mediator.Send(new GetTestFileByIdQuery { Id = id });
            if (file == null)
                return Json("Không tìm thấy file");
            //else
            //{
            //    FileUtils.DeleteFile(file.FilePath);
            //    await FileDataService.RemoveTestFile(id);
            //}
            var testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
            return Json(testId);
        }
        public IActionResult SelectCourses()
        {
            return View();
        }
        public IActionResult Submission(int submissionId = 0)
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save()
        {
            return View();
        }
        public async Task<IActionResult> Download(Guid id)
        {
            string studentId = "20T1020433";
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
            String teacherId = "1";
            if (files == null || files.Count == 0)
                return Json("Không có tệp nào được gửi.");
            else
            {
                int testId = 0;
                if (ApplicationContext.GetDataInt32(TESTID) == null || ApplicationContext.GetDataInt32(TESTID) == 0)
                {
                    testId = await _mediator.Send(new CreateTestCommand { TeacherId = teacherId, TestType = TestType.Exam, TestStatus = TestStatus.Creating });
                    ApplicationContext.SetInt32(TESTID, testId);
                }
                else
                {
                    testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
                }

                foreach (var item in files)
                {

                    await _mediator.Send(new CreateTestFileCommand { File = item, TestId = testId });
                }
                return Json(testId);
            }
        }
        public async Task<IActionResult> SelectStudents(CreateTestCommand command)
        {

            int testId = 0;
            if (ApplicationContext.GetDataInt32(TESTID) != null && ApplicationContext.GetDataInt32(TESTID) != 0)
            {
                testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
                var updateTestCommmand = _mapper.Map<UpdateTestCommand>(command);
                await _mediator.Send(updateTestCommmand);
            }
            else
            {
                testId = await _mediator.Send(command);
                HttpContext.Session.SetInt32(TESTID, testId);
            }

            return View(testId);
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
