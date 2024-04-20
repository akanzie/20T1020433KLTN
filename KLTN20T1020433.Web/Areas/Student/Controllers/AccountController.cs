﻿using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.AppCodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;

namespace KLTN20T1020433.Web.Areas.Student.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiConfig _apiOptions;
        private readonly HttpClient _httpClient;
        public AccountController(IOptions<ApiConfig> apiOptions, HttpClient httpClient)
        {
            _apiOptions = apiOptions.Value;
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            string loginUrl = $"{_apiOptions.Host}/auth/account/authorize?app_id={_apiOptions.AppId}&redirect_uri={Constants.REDIRECT_URI}&role={Constants.STUDENT_ROLE}";
            return Redirect(loginUrl);
        }
        public async Task<IActionResult> Callback(string code, string host)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            string time = DateTime.Now.ToString("yyyyMMddHHmmss");

            string signature = Utils.CalculateSignature(_apiOptions.AppId, _apiOptions.SecretKey, time); // Tính chữ ký bằng cách gọi hàm CalculateSignature

            // Tạo tham số truyền vào lời gọi API
            string apiUrl = $"{host}?code={code}&time={time}&signature={signature}";
            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, null);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                // Lưu trữ token vào biến session hoặc cache để sử dụng sau này
                string token = responseData.Data.Token;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Handle error
                return StatusCode((int)response.StatusCode);

            }
        }
    }
}
