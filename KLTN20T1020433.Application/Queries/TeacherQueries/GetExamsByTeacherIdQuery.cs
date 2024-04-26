﻿using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetExamsByTeacherIdQuery : IRequest<IEnumerable<GetExamResponse>>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
        public string TeacherId { get; set; }
    }
    public class GetExamsByTeacherIdQueryHandler : IRequestHandler<GetExamsByTeacherIdQuery, IEnumerable<GetExamResponse>>
    {
        private readonly ApiService _apiService;

        public GetExamsByTeacherIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetExamResponse>> Handle(GetExamsByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            string endpoint = "account/v1/courses";
            string jsonResponse = await _apiService.SendAsync(endpoint, request.GetTokenResponse);
            if (jsonResponse != null)
            {
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                IEnumerable<GetExamResponse> courses = JsonConvert.DeserializeObject<GetExamResponse>(responseData.Data.ToString())!;
                return courses;
            }
            return new List<GetExamResponse>();
        }
    }
}