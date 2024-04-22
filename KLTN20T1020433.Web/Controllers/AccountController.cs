using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.Queries.StudentQueries;
using KLTN20T1020433.Application.Queries;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.Web.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace KLTN20T1020433.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiConfig _apiOptions;
        private readonly IMediator _mediator;
        public AccountController(IOptions<ApiConfig> apiOptions, IMediator mediator)
        {
            _apiOptions = apiOptions.Value;
            _mediator = mediator;
        }
        public IActionResult Login()
        {
            string loginUrl = $"{_apiOptions.Host}/auth/account/authorize?app_id={_apiOptions.AppId}&redirect_uri={Constants.REDIRECT_URI}";
            return Redirect(loginUrl);
        }
        public async Task<IActionResult> Callback(string code, string host)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            var getTokenResponse = await _mediator.Send(new GetTokenQuery { Host = host, Code = code, Time = time });
            ApplicationContext.SetSessionData(Constants.ACCESS_TOKEN, getTokenResponse);
            var profile = await _mediator.Send(new GetProfileByTokenQuery { GetTokenResponse = getTokenResponse });
            WebUserData userData = new WebUserData()
            {
                UserId = profile.Role == Constants.STUDENT_ROLE ? profile.StudentId : profile.TeacherId,
                DisplayName = profile.LastName + " " + profile.FirstName,
                Email = profile.Email,
                ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                SessionId = HttpContext.Session.Id,
                Role = profile.Role
            };
            await HttpContext.SignInAsync(userData.CreatePrincipal());            
            return RedirectToAction("Index", "Home", new { area = "Student" });
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}