using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.Queries;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Services
{
    public class ApiService
    {
        protected readonly HttpClient _httpClient;
        private readonly ApiConfig _apiOptions;

        public ApiService(HttpClient httpClient, IOptions<ApiConfig> apiOptions)
        {
            _httpClient = httpClient;
            _apiOptions = apiOptions.Value;
        }
       
        public async Task<string> SendAsync(string endpoint, string token, string signature)
        {
            HttpRequestMessage request = await CreateRequest(endpoint, token, signature);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
        protected async Task<HttpRequestMessage> CreateRequest(string endpoint, string token, string signature)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiOptions.Host}/{endpoint}");

                // Conditionally add headers only if they are provided
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Add("ums-token", token);
                }

                request.Headers.Add("ums-application", _apiOptions.AppId);
                request.Headers.Add("ums-time", _apiOptions.SecretKey);

                if (!string.IsNullOrEmpty(signature))
                {
                    request.Headers.Add("ums-signature", signature);
                }

                var content = new StringContent(string.Empty);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = content;

                return request;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ khi tạo yêu cầu: {ex.Message}");
                throw;
            }
        }

    }
}
