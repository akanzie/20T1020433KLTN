using KLTN20T102433.Application.AppCodes;
using KLTN20T102433.Application.Models;
using KLTN20T102433.BussinessLayers;
using KLTN20T102433.Domain.Entities;
using KLTN20T102433.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T102433.Application.Controllers.Teacher
{
    public class TeacherTestController : Controller
    {
        const int TEST_PAGE_SIZE = 10;
        const string TEST_SEARCH = "test_search";

        public IActionResult Detail(int testId = 0)
        {
            var test = TeacherService.GetTest(testId);
            if (test == null)
            {
                return RedirectToAction("Index");
            }

            var files = TeacherService.GetFilesOfTest(testId);
            var model = new TestModel()
            {
                Test = test,
                Files = files
            };

            return View(model);

        }
        public IActionResult ListSubmission(int testId = 0)
        {
            var test = TeacherService.GetTest(testId);
            if (test == null)
            {
                return RedirectToAction("Index");
            }

            var model = TeacherService.GetSubmissionsOfTest(testId);
            return View(model);
        }
        public IActionResult CreateQuiz()
        {
            ViewBag.Title = "Tạo bài kiểm tra";
            ViewBag.IsEdit = false;
            var model = new Test()
            {
                TestId = 0,
                TestType = TestType.Quiz
            };

            return View("CreateExam", model);
        }
        public IActionResult CreateExam()
        {
            ViewBag.Title = "Tạo bài thi";
            ViewBag.IsEdit = false;
            var model = new Test()
            {
                TestId = 0,
                TestType = TestType.Exam
            };

            return View("CreateExam", model);
        }
        public IActionResult ListTest()
        {
            TestSearchInput? input = ApplicationContext.GetSessionData<TestSearchInput>(TEST_SEARCH);
            if (input == null)
            {
                input = new TestSearchInput()
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
        public IActionResult Search(TestSearchInput input)
        {
            int rowCount = 0;
            string teacherId = "";
            var data = TeacherService.GetTestsOfTeacher(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "", teacherId, input.Type, input.Status,
                                            input.FromTime, input.ToTime);

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
        public IActionResult SelectStudents()
        {
            return View();
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
        public IActionResult Save()
        {
            return View();
        }
        public IActionResult Download()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadTestFile(IFormFile file)
        {
            String teacherId = "123";
            if (file != null && file.Length > 0)
            {
                int testId;
                if (HttpContext.Session.TryGetValue("TestId", out byte[] testIdBytes))
                {
                    // Nếu đã có testId, chuyển từ byte[] thành Guid
                    testId = BitConverter.ToInt32(testIdBytes, 0);
                }
                else
                {
                    // Nếu chưa có testId, tạo một bản ghi test và lấy testId từ đó

                    testId = TeacherService.CreateTest(teacherId);
                    HttpContext.Session.SetInt32("TestId", testId);
                }
                var uploadedFile = new TestFile
                {
                    FileId = Guid.NewGuid(),
                    FileName = Path.GetFileName(file.FileName),
                    MimeType = file.ContentType,
                    Size = file.Length,
                    TestId = testId
                };

                // Tạo đường dẫn lưu trữ trên server
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uploadedFile.FileId.ToString());

                // Lưu file vào đường dẫn vừa tạo
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Lưu thông tin về file vào CSDL
                TeacherService.UploadTestFile(testId, uploadedFile);

                return View("CreateExam");
            }

            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> SelectStudents(Test model)
        {
            int testId;

            // Kiểm tra xem testId đã được lưu trong session chưa
            if (HttpContext.Session.TryGetValue("TestId", out byte[] testIdBytes))
            {
                // Nếu đã có testId, chuyển từ byte[] thành int
                testId = BitConverter.ToInt32(testIdBytes, 0);
                model.TestId = testId;
                // Cập nhật bản ghi test
                bool result = TeacherService.EditTest(model);
            }
            else
            {
                // Nếu chưa có testId, tạo một bản ghi test mới
                var test = new Test
                {
                    Title = model.Title,
                    Instruction = model.Instruction,
                    IsCheckIP = model.IsCheckIP,
                    IsConductedAtSchool = model.IsConductedAtSchool,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    TeacherId = model.TeacherId,
                    CreatedTime = DateTime.Now,
                    TestType = TestType.Exam,

                };

                testId = TeacherService.InitTest(test);


                // Lưu testId vào session để sử dụng cho các lần upload file sau
                HttpContext.Session.SetInt32("TestId", test.TestId);

                testId = test.TestId;
            }

            // Redirect đến trang chọn sinh viên
            return RedirectToAction("SelectStudents", new { TestId = testId });
        }
    }

}
