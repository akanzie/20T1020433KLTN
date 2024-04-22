using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries
{
    public class GetProfileByTokenQuery : IRequest<GetProfileResponse>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
    }
    public class GetProfileByTokenQueryHandler : IRequestHandler<GetProfileByTokenQuery, GetProfileResponse>
    {
        private readonly ApiService _apiService;

        public GetProfileByTokenQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<GetProfileResponse> Handle(GetProfileByTokenQuery request, CancellationToken cancellationToken)
        {
            string endpoint = "account/v1/profile";
            string jsonResponse = await _apiService.SendAsync(endpoint, request.GetTokenResponse);
            if (jsonResponse != null)
            {
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                GetProfileResponse profile = JsonConvert.DeserializeObject<GetProfileResponse>(responseData.Data.ToString())!;
                return profile;
            }
            return new GetProfileResponse();

        }
    }
}
