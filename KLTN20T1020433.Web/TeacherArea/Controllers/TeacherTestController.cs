using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Models;
using KLTN20T1020433.BusinessLayers;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace KLTN20T1020433.Web.Controllers.Teacher
{
    public class TeacherTestController : Controller
    {
        const int TEST_PAGE_SIZE = 10;
        const string TEST_SEARCH = "test_search";
        const string TESTID = "testId";
        public async Task<IActionResult> Detail(int testId = 0)
        {
            var test = await TeacherService.GetTest(testId);
            if (test == null)
            {
                return RedirectToAction("Index");
            }

            var files = await FileDataService.GetFilesOfTest(testId);
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
            var model = new Test()
            {
                TestId = testId,
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
        public async Task<IActionResult> Search(TestSearchInput input)
        {
            string teacherId = "";
            int rowCount = await TeacherService.GetRowCount(teacherId, input.SearchValue ?? "", input.Type, input.Status,
                                            input.FromTime, input.ToTime);

            var data = await TeacherService.GetTestsOfTeacher(input.Page, input.PageSize, input.SearchValue ?? "", teacherId, input.Type, input.Status,
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
        [HttpPost]
        public async Task<IActionResult> RemoveTestFile(Guid id)
        {
            TestFile? file = await FileDataService.GetTestFile(id);
            if (file == null)
                return Json("Không tìm thấy file");
            else
            {
                FileUtils.DeleteFile(file.FilePath);
                await FileDataService.RemoveTestFile(id);
            }
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
            bool isAuthorized = await FileDataService.CheckFileAuthorize(studentId, id);
            if (isAuthorized)
            {
                var fileInfo = await FileDataService.GetSubmissionFile(id);

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
                    Test test = new Test()
                    {
                        TeacherId = teacherId,
                        TestType = TestType.Exam,
                        Status = TestStatus.Creating,
                        CreatedTime = DateTime.Now,
                    };
                    testId = await TeacherService.CreateTest(test);
                    ApplicationContext.SetInt32(TESTID, testId);                    
                }
                else
                {
                    testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
                }
                
                foreach (var item in files)
                {
                    TestFile testFile = await FileUtils.SaveTestFileAsync(item, testId);
                    await FileDataService.AddTestFile(testFile);
                }
                return Json(testId);
            }
        }
        public async Task<IActionResult> SelectStudents(Test model)
        {
            
            int testId = 0;
            if (ApplicationContext.GetDataInt32(TESTID) != null && ApplicationContext.GetDataInt32(TESTID) != 0)
            {
                testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
                Test? test = await TeacherService.GetTest(testId);
                model.TestId = testId;
                model.TeacherId = test!.TeacherId;                
                model.TestType = TestType.Exam;
                model.Status = TestStatus.Creating;
                model.CreatedTime = DateTime.Now;
                bool result = await TeacherService.UpdateTest(model);
            }
            else
            {                
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
                testId = await TeacherService.CreateTest(test);
                HttpContext.Session.SetInt32(TESTID, testId);
            }

            return View(testId);
        }
        public async Task<IActionResult> ListTestFiles(int testId = 0)
        {
            //testId = ApplicationContext.GetDataInt32(TESTID) ?? 0;
            var test = await TeacherService.GetTest(testId);
            if (test != null)
            {
                var files = await FileDataService.GetFilesOfTest(testId);               

                var model = new TestFileModel
                {
                    Test = test,
                    Files = files

                };
                return PartialView(model);
            }
            return BadRequest("Có lỗi xảy ra.");
        }
    }


}
