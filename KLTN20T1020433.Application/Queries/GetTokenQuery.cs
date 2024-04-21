
using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.Services;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries
{
    public class GetTokenQuery : IRequest<GetTokenResponse>
    {
        public string Host { get; set; }
        public string Code { get; set; }
        public string Time { get; set; }
    }
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, GetTokenResponse>
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfig _apiOptions;

        public GetTokenQueryHandler(HttpClient httpClient, IOptions<ApiConfig> apiOptions)
        {
            _httpClient = httpClient;
            _apiOptions = apiOptions.Value;
        }
        public async Task<GetTokenResponse> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            string signature = Utils.CalculateSignature(_apiOptions.AppId, _apiOptions.SecretKey, request.Time);
            string apiUrl = $"{request.Host}?code={request.Code}&time={request.Time}&signature={signature}";
            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                return new GetTokenResponse { Signature = signature, Token = responseData.Data.Token.ToString() };
            }
            else
            {
                throw new HttpRequestException($"API request failed with status code {(int)response.StatusCode}");
            }
        }
    }

}
