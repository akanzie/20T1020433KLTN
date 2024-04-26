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
        protected async Task<HttpRequestMessage> CreateRequest(string endpoint, GetTokenResponse getTokenResponse)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiOptions.Host}/{endpoint}");
           request.Headers.Add("ums-token", getTokenResponse.Token);
            request.Headers.Add("ums-application", _apiOptions.AppId);
            request.Headers.Add("ums-time", _apiOptions.SecretKey);
            request.Headers.Add("ums-signature", getTokenResponse.Signature);
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            return request;
        }
        public async Task<string> SendAsync(string endpoint, GetTokenResponse getTokenResponse)
        {
            HttpRequestMessage request = await CreateRequest(endpoint, getTokenResponse);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
    }
}
