using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetStudentProfileByTokenQuery : IRequest<GetStudentProfileResponse>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
    }
    public class GetStudentProfileByTokenQueryHandler : IRequestHandler<GetStudentProfileByTokenQuery, GetStudentProfileResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfig _apiOptions;

        public GetStudentProfileByTokenQueryHandler(HttpClient httpClient, IOptions<ApiConfig> apiOptions)
        {
            _httpClient = httpClient;
            _apiOptions = apiOptions.Value;
        }
        public async Task<GetStudentProfileResponse> Handle(GetStudentProfileByTokenQuery request, CancellationToken cancellationToken)
        {            
            var requestAPI = new HttpRequestMessage(HttpMethod.Get, $"{_apiOptions.Host}/account/v1/profile");
            requestAPI.Headers.Add("ums-token", request.GetTokenResponse.Token);
            requestAPI.Headers.Add("ums-application", _apiOptions.AppId);
            requestAPI.Headers.Add("ums-time", _apiOptions.SecretKey);
            requestAPI.Headers.Add("ums-signature", request.GetTokenResponse.Signature);
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestAPI.Content = content;
            var response = await _httpClient.SendAsync(requestAPI);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            GetStudentProfileResponse profile = JsonSerializer.Deserialize<GetStudentProfileResponse>(jsonResponse);
            return profile;
        }
    }
}
