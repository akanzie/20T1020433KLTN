using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace KLTN20T1020433.Web.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly HttpClient _httpClient;
        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            string loginUrl = $"{HOST}/auth/account/authorize?app_id={APP_ID}&redirect_uri={REDIRECT_URI}";
            return Redirect(loginUrl);
        }
        public async Task<IActionResult> Callback(string code, string host)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            string time = DateTime.Now.ToString("yyyyMMddHHmmss");

            string signature = Utils.CalculateSignature(APP_ID, SECRET_KEY, time); // Tính chữ ký bằng cách gọi hàm CalculateSignature

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

    public class ApiResponse
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public DataObject Data { get; set; }
    }

    public class DataObject
    {
        public string AppId { get; set; }
        public string Token { get; set; }
    }
}