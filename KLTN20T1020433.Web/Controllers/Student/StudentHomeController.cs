using KLTN20T1020433.BusinessLayers;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.Controllers.Student
{
    public class StudentHomeController : Controller
    {
        const int PAGE_SIZE = 10;
        const string TEST_PAGINATION = "test_pagination";

        public IActionResult Index()
        {
            Models.TestPagination? input = ApplicationContext.GetSessionData<TestPagination>(TEST_PAGINATION);
            if (input == null)
            {
                input = new TestPagination()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    StudentId = "20T1020433"
                };

            }
            return View(input);
        }
        public IActionResult Pagination(TestPagination input)
        {
            int rowCount = 0;
            
            var data = StudentService.GetTestsForStudentHome(out rowCount, input.Page, input.PageSize, input.StudentId ?? "20T1020433");

            var model = new TestSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                Data = data
            };

            // Lưu lại vào session điều kiện tìm kiếm
            ApplicationContext.SetSessionData(TEST_PAGINATION, input);

            return View(model);

        }
    }
}
