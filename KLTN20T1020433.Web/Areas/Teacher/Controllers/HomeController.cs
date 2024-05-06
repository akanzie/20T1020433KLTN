using KLTN20T1020433.Application.Queries.TeacherQueries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Areas.Student.Models;
using KLTN20T1020433.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace KLTN20T1020433.Web.Controllers
{
    [Area("Teacher")]
    [Authorize]
    [TeacherOnlyFilter]
    public class HomeController : Controller
    {

        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index(string teacherId)
        {
            try
            {
                var courses = await _mediator.Send(new GetCoursesByTeacherIdQuery { TeacherId = teacherId });
                return View(courses);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in Index: {ex.Message}");
                return View("Error", new ErrorMessageModel { Title = "Đã xảy ra lỗi không mong muốn", Content = ErrorMessages.RequestNotCompleted });
            }
        }




    }
}
