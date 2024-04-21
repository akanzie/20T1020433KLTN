using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.API
{
    public abstract class _BaseApi
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;

        public _BaseApi(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }
        protected async Task<HttpRequestMessage> CreateRequest(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiOptions.Host}/{endpoint}");
            request.Headers.Add("ums-token", request.GetTokenResponse.Token);
            request.Headers.Add("ums-application", _apiOptions.AppId);
            request.Headers.Add("ums-time", _apiOptions.SecretKey);
            request.Headers.Add("ums-signature", request.GetTokenResponse.Signature);
            return request;
        }
        protected async Task<string> SendAsync(string endpoint)
        {
            
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestAPI.Content = content;
            var response = await _httpClient.SendAsync(requestAPI);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
            }
        }        
    }
}
