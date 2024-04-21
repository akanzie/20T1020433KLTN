using KLTN20T1020433.Application.Configuration;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Application.Services;
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
        private readonly ApiService _apiService;

        public GetStudentProfileByTokenQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<GetStudentProfileResponse> Handle(GetStudentProfileByTokenQuery request, CancellationToken cancellationToken)
        {
            string endpoint = "account/v1/profile";
            string jsonResponse = await _apiService.SendAsync(endpoint, request.GetTokenResponse);
            GetStudentProfileResponse profile = JsonSerializer.Deserialize<GetStudentProfileResponse>(jsonResponse);
            return profile;
        }
    }
}
