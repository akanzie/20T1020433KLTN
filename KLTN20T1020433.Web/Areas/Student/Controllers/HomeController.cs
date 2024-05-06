
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
    public class HomeController : Controller
    {
        const int PAGE_SIZE = 10;
        const string TEST_PAGINATION = "test_pagination";
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            var user = User.GetUserData();
            Models.TestPagination? input = ApplicationContext.GetSessionData<TestPagination>(TEST_PAGINATION);
            if (input == null)
            {
                input = new TestPagination()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    StudentId = user.UserId
                };
            }
            return View(input);
        }
        public async Task<IActionResult> Pagination(TestPagination input)
        {
            try
            {
                var user = User.GetUserData();
                var data = await _mediator.Send(new GetTestsBySearchQuery { Page = input.Page, PageSize = input.PageSize, StudentId = user.UserId });
                int rowCount = await _mediator.Send(new GetRowCountQuery { StudentId = user.UserId });
                var model = new TestSearchResult()
                {
                    Page = input.Page,
                    PageSize = input.PageSize,
                    RowCount = rowCount,
                    Data = data
                };
                ApplicationContext.SetSessionData(TEST_PAGINATION, input);
                return View(model);
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Exception occurred in Pagination: {ex.Message}");
                return Json(ErrorMessages.RequestNotCompleted);
            }
        }
    }
}
